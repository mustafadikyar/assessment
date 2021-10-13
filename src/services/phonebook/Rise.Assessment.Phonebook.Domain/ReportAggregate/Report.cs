using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rise.Assessment.Phonebook.Domain.ReportAggregate
{
    public enum ReportStatus
    {
        Creating,
        Completed
    }

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
