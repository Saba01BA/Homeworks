using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LoanApi.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseSqlServer(
                "Server=localhost\\MSSQLSERVER01;Database=LoanApiSimpleDb;Trusted_Connection=True;TrustServerCertificate=True");

            return new AppDbContext(options.Options);
        }
    }
}
