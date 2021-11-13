using Elastic.Apm.SerilogEnricher;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Linq;
using System.Reflection;

namespace HelloLogging
{
    public class Program
    {
        private static string ServiceName
        {
            get
            {
                var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                var serviceBaseName = string.Join('.', assemblyName.Split('.')?.Take(5));
                return serviceBaseName;
            }
        }

        public static void Main(string[] args)
        {
            ConfigureLogging();

            CreateHost(args);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog((hostContext, loggerConfiguration) =>
                {
                    loggerConfiguration.Enrich.WithProperty("ServiceSource", ServiceName);
                    loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
                    loggerConfiguration.Enrich.FromLogContext();
                });

        private static void CreateHost(string[] args)
        {
            try
            {
                Log.Information("Starting-up service: {ServiceName}", Assembly.GetExecutingAssembly().GetName().Name);
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to start service: {ServiceName}", Assembly.GetExecutingAssembly().GetName().Name);
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithElasticApmCorrelationInfo()
                .CreateLogger();
        }
    }
}
