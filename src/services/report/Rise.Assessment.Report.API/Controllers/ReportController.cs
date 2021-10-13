using Microsoft.AspNetCore.Mvc;
using Rise.Assessment.Report.API.Models.Contexts;
using Rise.Assessment.Report.API.Models.DTOs;
using Rise.Assessment.Report.API.Services;
using System;
using System.Threading.Tasks;

namespace Rise.Assessment.Report.API.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportDbContext _context;
        private readonly ReportPublisher _rabbitMQPublisher;
        public ReportController(ReportDbContext context, ReportPublisher rabbitMQPublisher)
        {
            _context = context;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpGet]
        public async Task<IActionResult> CreateReportAsync()
        {
            var report = new Models.Entities.Report
            {
                FileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm"),
                Status = Models.Enums.ReportStatus.Creating
            };

            await _context.AddAsync(report);
            await _context.SaveChangesAsync();

            _rabbitMQPublisher.Publish(new CreateReportMessage { ReportId = report.ReportId });

            return Ok(report);
        }
    }
}
