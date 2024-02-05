using IdentityDomain.Entities;
using IdentityProvider.Exceptions;
using IdentityProvider.Models.Request;
using IdentityProvider.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityProvider.Services
{
    public class IdentityProviderService
    {
        private readonly IServiceScope _scope;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public IdentityProviderService(IServiceProvider services, IConfiguration configuration)
        {
            _scope = services.CreateScope();
            _configuration = configuration;
            _userManager = _scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        }

        public async Task<User> Register(UserCreateModel userModel)
        {
            var user = new User
            {
                UserName = userModel.Email.ToUpperInvariant(),
                Email = userModel.Email,
                Name = userModel.Name,
            };

            var searchEmailResult = await _userManager.FindByEmailAsync(userModel.Email);
            if (searchEmailResult != null)
            {
                throw new ExistingEmailException();
            }

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                throw new BadRequestException(result.Errors.ToString());
            }

            await _userManager.AddToRoleAsync(user, "User");

            return user;
        }

        public async Task<LoginModel> Login(UserLoginModel userModel)
        {
            var user = await _userManager.FindByNameAsync(userModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, userModel.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim("fullName", user.Name),

                    new Claim(ClaimTypes.Name, user.Email),

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                authClaims.Add(new Claim("Oid", user.Id));

                var token = GetToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

                await _userManager.UpdateAsync(user);

                return new LoginModel
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    AccessTokenExpiration = token.ValidTo
                };
            }
            throw new UnauthorizedException();
        }

        public async Task<RefreshedTokenModel> RefreshToken(RefreshTokenModel model)
        {
            if (model == null)
            {
                throw new BadRequestException("Invalid client request");
            }
            string? accessToken = model.AccessToken;
            string? refreshToken = model.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                throw new BadRequestException("Invalid access token or refresh token");
            }

            var name = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(name);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {

                throw new BadRequestException("Invalid access token or refresh token");
            }

            var newAccessToken = GetToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            return new RefreshedTokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                AccessTokenExpiration = newAccessToken.ValidTo,
                RefreshToken = newRefreshToken
            };
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                //expires: DateTime.Now.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }
    }
}
