using Microsoft.EntityFrameworkCore;
using RespondentDataTracker.Models;
using WebApplication1.Models;

namespace RespondentDataTracker.Context
{
    public class PersonContext:DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost\\MSSQLSERVER01;Database=RespondentDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .OwnsOne(person => person.PersonAdress);
        }
    }
}
