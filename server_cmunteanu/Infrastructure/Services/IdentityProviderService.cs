using System.Net.Http.Json;
using Application.Models;
using Application.Models.Request;
using Application.Models.Response;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class IdentityProviderService
    {
        private readonly IProducerService _producerService;

        public IdentityProviderService(IProducerService producerService) 
        {
            _producerService = producerService;
        }

        public async Task<ResponseModel> Register(UserCreateModel userModel)
        {
            var requestModel = new RequestModel { Type = RequestType.Register, Payload = JsonConvert.SerializeObject(userModel) };
            var response = await _producerService.SendMessage(requestModel);
            return response;
        }

        public async Task<ResponseModel> Login(UserLoginModel userModel)
        {
            var requestModel = new RequestModel { Type = RequestType.Login, Payload = JsonConvert.SerializeObject(userModel) };
            var response = await _producerService.SendMessage(requestModel);
            return response;
        }

        public async Task<ResponseModel> RefreshToken(RefreshTokenModel refreshTokenModel)
        {
            var requestModel = new RequestModel { Type = RequestType.RefreshToken, Payload = JsonConvert.SerializeObject(refreshTokenModel) };
            var response = await _producerService.SendMessage(requestModel);
            return response;

        }
    }
}
