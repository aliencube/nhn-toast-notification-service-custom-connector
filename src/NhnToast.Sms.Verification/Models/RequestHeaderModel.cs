using Newtonsoft.Json;

namespace NhnToast.Sms.Verification.Models
{
    public class RequestHeaderModel
    {
        [JsonProperty("x-app-key")]
        public virtual string AppKey { get; set; }

        [JsonProperty("x-secret-key")]
        public virtual string SecretKey { get; set; }
    }
}