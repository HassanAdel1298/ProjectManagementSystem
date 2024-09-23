



using MediatR;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.ViewModel.RabbitMQMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ProjectManagementSystem.API.Services
{
    public class RabbitMQConsumerService : IHostedService
    {
        IConnection _connection;
        IModel _channel;
        IMediator _mediator;

        public RabbitMQConsumerService(IMediator mediator)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _mediator = mediator;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += Consumer_Received;

            _channel.BasicConsume("Projects", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            try
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                var baseMessage = GetBaseMessgae(message);

                await InvokeConsumer(baseMessage);

                _channel.BasicAck(e.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                // log exception

                _channel.BasicReject(e.DeliveryTag, requeue: true);
            }
        }

        private async Task InvokeConsumer(BaseMessage baseMessage)
        {
            var nameSpace = "ProjectManagementSystem.API.Services.RabbitMQServices";

            var consumerType = Type.GetType($"{nameSpace}.{baseMessage.Type},ProjectManagementSystem.API");

            if (consumerType is null)
                throw new Exception();

            var consumer = Activator.CreateInstance(consumerType, _mediator);
            var method = consumerType.GetMethod("Consume");

            method.Invoke(consumer, new object[] { baseMessage });
        }

        

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private BaseMessage GetBaseMessgae(string message)
        {
            var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(message);
            var typeName = jsonObject["Type"].ToString();
            var nameSpace = "ProjectManagementSystem.Application.ViewModel.RabbitMQMessages";

            Type type = Type.GetType($"{nameSpace}.{typeName},ProjectManagementSystem.Application");

            if (type is null)
                throw new Exception();

            var baseMessage = Newtonsoft.Json.JsonConvert.DeserializeObject(message, type) as BaseMessage;

            baseMessage.Type = typeName.Replace("Message", "Consumer");

            return baseMessage;
        }
    }
}
