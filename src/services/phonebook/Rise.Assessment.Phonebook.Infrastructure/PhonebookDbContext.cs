using Microsoft.EntityFrameworkCore;
using Rise.Assessment.Phonebook.Domain.PhonebookAggregate;

namespace Rise.Assessment.Phonebook.Infrastructure
{
    public class PhonebookDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "phonebook";

        public PhonebookDbContext(DbContextOptions<PhonebookDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonDetail> PersonDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person", DEFAULT_SCHEMA);
            modelBuilder.Entity<PersonDetail>().ToTable("PersonDetails", DEFAULT_SCHEMA);

            base.OnModelCreating(modelBuilder);
        }
    }
}
