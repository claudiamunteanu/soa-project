using System.Net.Http.Json;
using Application.Models.Request;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public interface IEmailService
    {
        public Task<HttpResponseMessage> SendConfirmationEmail(Order order);
    }
    public class EmailService : IEmailService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public EmailService(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
            _httpClient = new HttpClient();
            var accessToken = _configuration["SendConfirmationEmailAccessKey"];
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", accessToken);
        }

        public async Task<HttpResponseMessage> SendConfirmationEmail(Order order)
        {
            string url = _configuration["EmailApi:SendEmailConfirmation"];
            var emailConfirmationModel = _mapper.Map<EmailConfirmationModel>(order);
            return await _httpClient.PostAsJsonAsync(url, emailConfirmationModel);
        }
    }
}
