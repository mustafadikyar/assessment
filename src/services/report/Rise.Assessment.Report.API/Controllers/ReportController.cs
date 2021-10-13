using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rise.Assessment.Report.API.Models.Contexts;
using Rise.Assessment.Report.API.Models.DTOs;
using Rise.Assessment.Report.API.Services;
using System;
using System.IO;
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

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int reportId)
        {
            if (file is not { Length: > 0 }) return BadRequest();

            Models.Entities.Report report = await _context.Report.FirstAsync(report => report.ReportId == reportId);
            string filePath = report.FileName + Path.GetExtension(file.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", filePath);

            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);

            report.CreatedTime = DateTime.Now;
            report.FilePath = filePath;
            report.Status = Models.Enums.ReportStatus.Completed;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
