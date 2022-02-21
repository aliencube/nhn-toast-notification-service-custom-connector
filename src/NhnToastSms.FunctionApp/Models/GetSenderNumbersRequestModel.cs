using Newtonsoft.Json;

namespace NhnToastSms.FunctionApp.Models
{
    public class GetSenderNumbersRequestModel
    {
        [JsonProperty("sendNo")]
        public virtual string SenderNumber { get; set; }

        [JsonProperty("useYn")]
        public virtual string UseNumber { get; set; }

        [JsonProperty("blockYn")]
        public virtual string BlockedNumber { get; set; }

        [JsonProperty("pageNum")]
        public virtual int? PageNumber { get; set; }

        public virtual int? PageSize { get; set; }
    }
}