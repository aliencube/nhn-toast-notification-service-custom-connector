using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace NhnToastSms.FunctionApp.Configurations
{
    public class AppSettings
    {
        public virtual OpenApiSettings OpenApi { get; set; }
        public virtual ToastSettings Toast { get; set; }
    }

    public class OpenApiSettings
    {
        public const string Name = "OpenApi";

        public virtual OpenApiVersionType Version { get; set; }
        public virtual string DocVersion { get; set; }
    }

    public class ToastSettings
    {
        public const string Name = "Toast";

        public virtual string HostName { get; set; }
        public virtual string BaseUrl { get; set; }
        public virtual string ApiVersion { get; set; }
    }
}