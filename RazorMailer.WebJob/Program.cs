using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RazorMailer.Parsing;
using SendGrid.Helpers.Mail;

namespace RazorMailer.WebJob
{
    public class Program
    {
        private static IHost BuildHost(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration(Configure)
                .ConfigureHostConfiguration(Configure)
                .ConfigureWebJobs(ConfigureWebJobs)
                .ConfigureServices(ConfigureServices)
                .UseConsoleLifetime();
            
            return builder.Build();
        }

        private static int Main(string[] args)
        {
            var host = BuildHost(args);
            using (host)
            {
                host.Run();
            }

            return 0;
        }

        private static void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
        {
            services.AddSingleton<ITemplateParser, TemplateParser>();
        }

        private static void ConfigureWebJobs(IWebJobsBuilder builder)
        {
            var configBuilder = new ConfigurationBuilder();

            Configure(configBuilder);


            builder
                .AddAzureStorageCoreServices()
                .AddAzureStorage()
                .AddSendGrid(o =>
                {
                    o.ApiKey = configBuilder.Build()["AzureWebJobsSendGridApiKey"];
                    o.ToAddress = new EmailAddress("razormailer@mailinator.com", "RazorMailer");
                    o.FromAddress = new EmailAddress("noreply@mailinator.com", "RazorMailer");
                });

            builder.Services.AddLogging(config => config.AddConsole());
        }

        private static void Configure(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            if (IsDevelopment())
            {
                builder.AddUserSecrets<Program>();
            }
        }

        private static bool IsDevelopment()
        {
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToLower() == "development";
        }
    }
}