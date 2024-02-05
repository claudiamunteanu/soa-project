using Azure;
using IdentityProvider.Exceptions;
using IdentityProvider.Models;
using IdentityProvider.Models.Request;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System;
using System.Text;

namespace IdentityProvider.Services
{
    public class ConsumerService : IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;

        private readonly string _queueName = Environment.GetEnvironmentVariable("QUEUE_NAME");
        //private string _queueName = "rpc_queue";

        private IdentityProviderService _identityProviderService;

        public ConsumerService(IRabbitMqService rabbitMqService, IdentityProviderService identityProviderService)
        {
            _identityProviderService = identityProviderService;

            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(_queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _model.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }
        public void ConfigureConsumerService() { 

            var consumer = new EventingBasicConsumer(_model);
            _model.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
            Console.WriteLine(" [x] Awaiting RPC requests");
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                RequestModel requestModel = JsonConvert.DeserializeObject<RequestModel>(message);
                Console.WriteLine($" [.] RequestModel({requestModel})");

                ResponseModel response = new ResponseModel();

                if (requestModel != null)
                {
                    try
                    {
                        response = requestModel.Type switch
                        {
                            RequestType.Login => await HandleLoginRequest(requestModel.Payload),
                            RequestType.Register => await HandleRegisterRequest(requestModel.Payload),
                            RequestType.RefreshToken => await HandleRefreshTokenRequest(requestModel.Payload),
                            _ => throw new BadRequestException(),
                        };
                    }
                    catch (Exception exception)
                    {
                        HttpStatusCode code;
                        switch (exception)
                        {
                            case BadRequestException:
                                code = HttpStatusCode.BadRequest;
                                break;
                            case UnauthorizedException:
                                code = HttpStatusCode.Unauthorized;
                                break;
                            case ExistingEmailException:
                                code = HttpStatusCode.Conflict;
                                break;
                            default:
                                code = HttpStatusCode.InternalServerError;
                                break;
                        }
                        response = new ResponseModel
                        {
                            StatusCode = code,
                            IsSuccessStatusCode = false,
                            Payload = exception.Message
                        };
                    }
                }
                var replyProperties = _model.CreateBasicProperties();
                replyProperties.CorrelationId = ea.BasicProperties.CorrelationId;
                var responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                _model.BasicPublish(string.Empty, routingKey: ea.BasicProperties.ReplyTo, basicProperties: replyProperties, body: responseBytes);
                _model.BasicAck(ea.DeliveryTag, false);
            };
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }

        private async Task<ResponseModel> HandleLoginRequest(string payload)
        {
            var userLoginModel = JsonConvert.DeserializeObject<UserLoginModel>(payload);
            if (userLoginModel != null) 
            { 
                var loginModel = await _identityProviderService.Login(userLoginModel); 
                var responsePayload = JsonConvert.SerializeObject(loginModel);
                var responseModel = new ResponseModel { Payload = responsePayload };
                return responseModel;
            }
            throw new BadRequestException();
        }

        private async Task<ResponseModel> HandleRegisterRequest(string payload)
        {
            var userCreateModel = JsonConvert.DeserializeObject<UserCreateModel>(payload);
            if (userCreateModel != null)
            {
                var user = await _identityProviderService.Register(userCreateModel);
                var responsePayload = JsonConvert.SerializeObject(user);
                return new ResponseModel { Payload = responsePayload };
            }
            throw new BadRequestException();
        }

        private async Task<ResponseModel> HandleRefreshTokenRequest(string payload)
        {
            var refreshTokenModel = JsonConvert.DeserializeObject<RefreshTokenModel>(payload);
            if (refreshTokenModel != null)
            {
                var refreshedTokenModel = await _identityProviderService.RefreshToken(refreshTokenModel);
                var responsePayload = JsonConvert.SerializeObject(refreshedTokenModel);
                return new ResponseModel { Payload = responsePayload };
            }
            throw new BadRequestException();
        }
    }
}
