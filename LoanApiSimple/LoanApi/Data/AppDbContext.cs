using LoanApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.UserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            modelBuilder.Entity<Loan>()
                .HasOne(loan => loan.User)
                .WithMany(user => user.Loans)
                .HasForeignKey(loan => loan.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Loan>()
                .Property(loan => loan.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<User>()
                .Property(user => user.MonthlyIncome)
                .HasPrecision(18, 2);
        }
    }
}
