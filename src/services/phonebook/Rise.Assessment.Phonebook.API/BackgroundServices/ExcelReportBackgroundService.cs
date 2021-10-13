using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Rise.Assessment.Phonebook.API.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Assessment.Phonebook.API.BackgroundServices
{
    public class ExcelReportBackgroundService : BackgroundService
    {
        private readonly RabbitMQClientService _reportConsumerService;
        private IModel _channel;

        public ExcelReportBackgroundService(RabbitMQClientService reportConsumerService, IModel channel)
        {
            _reportConsumerService = reportConsumerService;
            _channel = channel;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _reportConsumerService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
