using BookManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BookManagementSystem.Context
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost\\MSSQLSERVER01;Database=BooksDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
        }
    }
}
