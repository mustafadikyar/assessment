using Microsoft.EntityFrameworkCore;
using Rise.Assessment.Phonebook.Domain.ReportAggregate;

namespace Rise.Assessment.Phonebook.Infrastructure
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        {

        }

        public DbSet<Report> Report { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().ToTable("Report");
            base.OnModelCreating(modelBuilder);
        }
    }
}
