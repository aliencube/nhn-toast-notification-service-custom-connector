using Newtonsoft.Json;

namespace NhnToastSms.FunctionApp.Models
{
    public class RequestHeaderModel
    {
        [JsonProperty("x-app-key")]
        public virtual string AppKey { get; set; }

        [JsonProperty("x-secret-key")]
        public virtual string SecretKey { get; set; }
    }
}