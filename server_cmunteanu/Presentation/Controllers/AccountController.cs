using Application.Commands.Users.AddUser;
using Application.Commands.Users.GetAllUsers;
using Application.Commands.Users.GetUser;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Presentation.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IdentityProviderService _identityProviderService;
        private readonly IMediator _mediator;
        private readonly MonitoringService _monitoringService;
        public AccountController(IMediator mediator, IdentityProviderService identityProviderService, MonitoringService monitoringService)
        {
            _mediator = mediator;
            _identityProviderService = identityProviderService;
            _monitoringService = monitoringService;
        }


        [HttpPost("register")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserCreateModel userModel)
        {
            var response = await _identityProviderService.Register(userModel);
            if (response.IsSuccessStatusCode)
            {
                var savedUser = (JObject)JsonConvert.DeserializeObject(response.Payload);
                userModel.Id = (string)savedUser["Id"];

                var command = new AddUserCommand
                {
                    UserModel = userModel,
                };
                await _mediator.Send(command);

                return Ok();
            }
            else
            {
                if (((int)response.StatusCode) == 409)
                {
                    return Conflict();
                }
                return BadRequest(response);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseModel), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginModel userModel)
        { 
            var response = await _identityProviderService.Login(userModel);
            if (response.IsSuccessStatusCode)
            {
                var responseObject = JsonConvert.DeserializeObject<LoginResponseModel>(response.Payload);
                await _monitoringService.MonitorEvent("User logged in!");
                return Ok(responseObject);
            }
            return Unauthorized();
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(TokenResponseModel), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel tokenModel)
        {
            var response = await _identityProviderService.RefreshToken(tokenModel);
            if (response.IsSuccessStatusCode)
            {
                var responseObject = JsonConvert.DeserializeObject<TokenResponseModel>(response.Payload);
                return Ok(responseObject);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrUser")]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> GetUser(string id)
        {
            var command = new GetUserByIdCommand
            {
                UserId = id
            };
            var user = await _mediator.Send(command);
            return Ok(user);
        }

        [HttpGet]
        //[Authorize(Policy = "AdminOrUser")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            var command = new GetAllUsersCommand();
            var users = await _mediator.Send(command);
            return Ok(users);
        }
    }
}
