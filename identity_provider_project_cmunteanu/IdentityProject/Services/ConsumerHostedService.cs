namespace IdentityProvider.Services
{
    public class ConsumerHostedService : IHostedService
    {
        private readonly ConsumerService _consumerService;

        public ConsumerHostedService(ConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumerService.ConfigureConsumerService();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
