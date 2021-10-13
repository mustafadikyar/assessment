using RabbitMQ.Client;
using System;

namespace Rise.Assessment.Phonebook.API.Services
{
    public class RabbitMQClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string QueueName = "queue-report";

        public RabbitMQClientService(ConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true }) return _channel;

            _channel = _connection.CreateModel();
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
