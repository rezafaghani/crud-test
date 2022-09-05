using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Hosting
{
    // ReSharper disable once UnusedType.Global
    // ReSharper disable once InconsistentNaming
    public static class IWebHostExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost,
            Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (IServiceScope scope = webHost.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                ILogger<TContext> logger = services.GetRequiredService<ILogger<TContext>>();
                TContext context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}",
                        typeof(TContext).Name);

                    int retryInSeconds = 5;
                    RetryPolicy retry = Policy.Handle<SqlException>()
                        .WaitAndRetryForever(
                            (_, _) => TimeSpan.FromSeconds(retryInSeconds),
                            (exception, retryAttempt, _, _) =>
                            {
                                logger.LogWarning(exception,
                                    "[{Prefix}] Exception {ExceptionType} with message {Message} detected on attempt {Retry}",
                                    typeof(TContext).Name, exception.GetType().Name, exception.Message, retryAttempt);
                            });
                    retry.Execute(() => InvokeSeeder(seeder, context, services));


                    logger.LogInformation("Migrated database associated with context {DbContextName}",
                        typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,
                        "An error occurred while migrating the database used on context {DbContextName}",
                        typeof(TContext).Name);
                }
            }

            return webHost;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
            IServiceProvider services)
            where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}