using RabbitMQ.Client;
using System;

namespace Rise.Assessment.Report.API.Services
{
    public class ReportService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string exchangeName = "ReportDirectExchange";
        public static string route = "report-route";
        public static string queueName = "queue-report";

        public ReportService(ConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true }) return _channel;

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: exchangeName, type: "direct", durable: true, autoDelete: false);
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(exchange: exchangeName, queue: queueName, routingKey: route);

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
