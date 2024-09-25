

using RabbitMQ.Client;
using System.Text;

namespace ProjectManagementSystem.Application.Services
{
    public class RabbitMQPublisherService
    {
        IConnection _connection;
        IModel _channel;
        public RabbitMQPublisherService() 
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("ProjectsExchange", ExchangeType.Fanout, true, false);
            _channel.QueueDeclare("Projects", true , false , autoDelete: false);
             
            _channel.QueueBind("Projects", "ProjectsExchange", "ProjectsKey");
        }

        public void PublishMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish("ProjectsExchange", "ProjectsKey", body: body);
        }

    }
}
