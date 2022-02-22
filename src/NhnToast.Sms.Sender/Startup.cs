using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Configurations.AppSettings.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NhnToast.Sms.Sender.Configurations;

[assembly: FunctionsStartup(typeof(NhnToast.Sms.Sender.Startup))]

namespace NhnToast.Sms.Sender
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder
                   .AddEnvironmentVariables();

            base.ConfigureAppConfiguration(builder);
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureAppSettings(builder.Services);
            ConfigureHttpClient(builder.Services);
        }

        private static void ConfigureAppSettings(IServiceCollection services)
        {
            var openApiSettings = services.BuildServiceProvider()
                                          .GetService<IConfiguration>()
                                          .Get<OpenApiSettings>(OpenApiSettings.Name);
            services.AddSingleton(openApiSettings);

            var toastSettings = services.BuildServiceProvider()
                                        .GetService<IConfiguration>()
                                        .Get<ToastSettings>(ToastSettings.Name);
            services.AddSingleton(toastSettings);
        }

        private static void ConfigureHttpClient(IServiceCollection services)
        {
            services.AddHttpClient("sender");
        }
    }
}