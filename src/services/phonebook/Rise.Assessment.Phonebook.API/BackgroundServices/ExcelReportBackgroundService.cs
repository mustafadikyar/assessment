using ClosedXML.Excel;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rise.Assessment.Phonebook.API.Services;
using System;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Assessment.Phonebook.API.BackgroundServices
{
    public class ExcelReportBackgroundService : BackgroundService
    {
        private readonly RabbitMQClientService _reportConsumerService;
        private IModel _channel;

        public ExcelReportBackgroundService(RabbitMQClientService reportConsumerService)
        {
            _reportConsumerService = reportConsumerService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _reportConsumerService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var createReportMessage = JsonSerializer.Deserialize<CreateReportMessage>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            using var ms = new MemoryStream();

            var wb = new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(GetTable("report"));

            wb.Worksheets.Add(ds);
            wb.SaveAs(ms);

            MultipartFormDataContent multipartFormDataContent = new();
            multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");
            var baseUrl = "https://localhost:44333/api/reports";

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync($"{baseUrl}?reportId={createReportMessage.ReportId}", multipartFormDataContent);
                
                if (response.IsSuccessStatusCode)
                {
                    _channel.BasicAck(@event.DeliveryTag, false);
                }
            }
        }

        private DataTable GetTable(string tableName)
        {
            DataTable table = new DataTable { TableName = tableName };
            table.Columns.Add("Title 1", typeof(int));
            table.Columns.Add("Title 2", typeof(string));

            for (int i = 0; i < 10; i++)
            {
                table.Rows.Add(i, $"Description {i}");
            }

            return table;
        }
    }

    public class CreateReportMessage
    {
        public int ReportId { get; set; }
    }
}
