using Application.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infrastructure.Services
{
    public interface IRabbitMqService
    {
        IConnection CreateChannel();
    }

    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfiguration _configuration;
        public RabbitMqService(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }
        public IConnection CreateChannel()
        {
            ConnectionFactory connection = new ConnectionFactory()
            {
                UserName = _configuration.Username,
                Password = _configuration.Password,
                HostName = _configuration.HostName,
                Port = _configuration.Port
            };
            var channel = connection.CreateConnection();
            return channel;
        }
    }
}
