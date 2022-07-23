using System.IO;
using M2c.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace M2c.Api.Infrastructure.Factories
{
    public class M2CDbContextFactory :IDesignTimeDbContextFactory<M2CDbContext>
    {
        public M2CDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<M2CDbContext>();

            optionsBuilder.UseSqlServer(config.GetConnectionString("ConnectionString"), o => o.MigrationsAssembly("M2c.Api"));

            return new M2CDbContext(optionsBuilder.Options);
        }
    }
}