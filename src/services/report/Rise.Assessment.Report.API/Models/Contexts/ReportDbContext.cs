using Microsoft.EntityFrameworkCore;

namespace Rise.Assessment.Report.API.Models.Contexts
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        {

        }

        public DbSet<Entities.Report> Report { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Report>().ToTable("Report");
            base.OnModelCreating(modelBuilder);
        }
    }
}
