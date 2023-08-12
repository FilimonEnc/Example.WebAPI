using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;

using System.Reflection;

namespace Example.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().CreateLogger();
            try
            {
                Log.Information($"Start {Assembly.GetExecutingAssembly().GetName()}");
                var host = CreateHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((context, services, configuration) =>
                {
                    var version = Assembly.GetExecutingAssembly().GetName().Version;
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                        .Enrich.WithProperty("Version", version!)
                        .Enrich.WithEnvironmentName()
                        .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                            .WithDefaultDestructurers()
                            .WithDestructurers(new[]
                            {
                                new DbUpdateExceptionDestructurer()
                            })
                        );
                });
    }
}
