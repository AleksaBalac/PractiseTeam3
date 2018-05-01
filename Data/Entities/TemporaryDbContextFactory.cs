using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Entities
{
    public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private readonly IConfiguration _configuration;

        public TemporaryDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer(connectionString,
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name));
            return new AppDbContext(builder.Options);
        }
    }
}
