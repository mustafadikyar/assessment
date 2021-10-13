using Rise.Assessment.Report.API.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rise.Assessment.Report.API.Models.Entities
{
    public class Report
    {
        public int ReportId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? CreatedTime { get; set; }
        public ReportStatus Status { get; set; }

        [NotMapped] public string GetCreatedTime => CreatedTime.HasValue ? CreatedTime.Value.ToShortDateString() : "-";
    }
}
