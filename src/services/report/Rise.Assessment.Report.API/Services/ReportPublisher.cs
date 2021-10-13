using RabbitMQ.Client;
using Rise.Assessment.Report.API.Models.DTOs;
using System.Text;
using System.Text.Json;

namespace Rise.Assessment.Report.API.Services
{
    public class ReportPublisher
    {
        private readonly ReportService _rabbitMQClientService;
        public ReportPublisher(ReportService rabbitMQClientService) => _rabbitMQClientService = rabbitMQClientService;

        public void Publish(CreateReportMessage model)
        {
            var channel = _rabbitMQClientService.Connect();
            var bodyString = JsonSerializer.Serialize(model);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(
                exchange: ReportService.exchangeName,
                routingKey: ReportService.route,
                basicProperties: properties,
                body: bodyByte);

        }
    }
}