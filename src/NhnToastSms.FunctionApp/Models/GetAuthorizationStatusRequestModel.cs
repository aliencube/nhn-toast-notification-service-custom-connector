using Newtonsoft.Json;

using NhnToastSms.FunctionApp.Enums;

namespace NhnToastSms.FunctionApp.Models
{
    public class GetAuthorizationStatusRequestModel
    {
        [JsonProperty("sendNo")]
        public virtual string SenderNumber { get; set; }

        public virtual AuthorizationStatusType? Status { get; set; }

        [JsonProperty("pageNum")]
        public virtual int? PageNumber { get; set; }

        public virtual int? PageSize { get; set; }
    }
}