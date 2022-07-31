using System;
using System.IO;
using M2c.Api;
using M2c.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

IConfiguration configuration = GetConfiguration();

Log.Logger = CreateSerilogLogger(configuration);

try
{
    Log.Information("Configuring web host ({ApplicationContext})...", M2c.Api.Program.AppName);
    IWebHost host = BuildWebHost(configuration, args);

    Log.Information("Applying migrations ({ApplicationContext})...", M2c.Api.Program.AppName);
    host.MigrateDbContext<M2CDbContext>((context, services) => { });

    Log.Information("Starting web host ({ApplicationContext})...", M2c.Api.Program.AppName);
    host.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", M2c.Api.Program.AppName);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

IWebHost BuildWebHost(IConfiguration config, string[] args)
{
    return WebHost.CreateDefaultBuilder(args)
        .CaptureStartupErrors(false)
        .ConfigureAppConfiguration(x => x.AddConfiguration(config))
        .UseStartup<Startup>()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseSerilog()
        .Build();
}

ILogger CreateSerilogLogger(IConfiguration config)
{
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", M2c.Api.Program.AppName)
        .Enrich.FromLogContext()
        .WriteTo.MSSqlServer(
            config.GetConnectionString("CustomerDb"),
            new MSSqlServerSinkOptions
            {
                TableName = "LogEvents",
                SchemaName = "dbo",
                AutoCreateSqlTable = true
            },
            configuration.GetSection("Serilog:ColumnOptions"),
            configuration,
            columnOptionsSection: configuration.GetSection("Serilog:SinkOptions"))
        .WriteTo.Console()
        .ReadFrom.Configuration(config)
        .CreateLogger();
}

IConfiguration GetConfiguration()
{
    string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    IConfigurationBuilder builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{env}.json", true, true)
        .AddEnvironmentVariables();


    return builder.Build();
}

namespace M2c.Api
{
    public class Program
    {
        public static string Namespace = typeof(Startup).Namespace;

        public static string AppName =
            Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
    }
}