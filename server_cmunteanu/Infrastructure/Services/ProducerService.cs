using Application.Models;
using Infrastructure.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Channels;

namespace Infrastructure.Services
{
    public interface IProducerService
    {
        Task<ResponseModel> SendMessage(RequestModel requestModel, CancellationToken cancellationToken = default);
    }

    public class ProducerService : IProducerService, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;

        private readonly ConcurrentDictionary<string, TaskCompletionSource<ResponseModel>> callbackMapper = new();

        private readonly string _queueName = Environment.GetEnvironmentVariable("QUEUE_NAME");
        private readonly string _replyQueueName = Environment.GetEnvironmentVariable("REPLY_QUEUE_NAME");

        //private string _queueName = "rpc_queue";
        //private string _replyQueueName = "reply_queue";

        public ProducerService(IRabbitMqService rabbitMqService)
        {
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(_replyQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_model);

            consumer.Received += (model, ea) =>
            {
                if (!callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                    return;

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var responseModel = JsonConvert.DeserializeObject<ResponseModel>(message);
                tcs.TrySetResult(responseModel);
            };

            _model.BasicConsume(_replyQueueName, true, consumer);
        }

        public async Task<ResponseModel> SendMessage(RequestModel requestModel, CancellationToken cancellationToken = default)
        {
            IBasicProperties props = _model.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = _replyQueueName;

            var requestBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestModel));
            var tcs = new TaskCompletionSource<ResponseModel>();
            callbackMapper.TryAdd(correlationId, tcs);

            _model.BasicPublish(string.Empty, _queueName, props, requestBytes);

            cancellationToken.Register(() => callbackMapper.TryRemove(correlationId, out _));

            return await tcs.Task;
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
