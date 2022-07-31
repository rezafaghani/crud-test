using System.IO;
using M2c.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace M2c.Api.Infrastructure.Factories
{
    public class M2CDbContextFactory : IDesignTimeDbContextFactory<M2CDbContext>
    {
        public M2CDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            DbContextOptionsBuilder<M2CDbContext> optionsBuilder = new();

            optionsBuilder.UseSqlServer(config.GetConnectionString("CustomerDb"), o => o.MigrationsAssembly("M2c.Api"));

            return new M2CDbContext(optionsBuilder.Options);
        }
    }
}