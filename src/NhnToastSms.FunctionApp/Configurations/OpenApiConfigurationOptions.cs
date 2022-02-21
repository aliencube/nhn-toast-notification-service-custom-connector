using System;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace NhnToastSms.FunctionApp.Configurations
{
    public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiInfo Info { get; set; } = new OpenApiInfo()
        {
            Version = GetOpenApiDocVersion(),
            Title = "NHN Toast Notification Service - SMS",
            Description = "This service is the system that sends SMS and MMS, manages message templates and message histories.",
            TermsOfService = new Uri("https://www.toast.com/kr/terms/terms-service"),
            Contact = new OpenApiContact()
            {
                Name = "Support",
                Email = "support@toast.com",
                Url = new Uri("https://docs.toast.com/en/Notification/SMS/en/service-policy/")
            }
        };

        public override OpenApiVersionType OpenApiVersion { get; set; } = GetOpenApiVersion();
    }
}