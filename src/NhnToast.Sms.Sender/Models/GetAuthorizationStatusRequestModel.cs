using Newtonsoft.Json;

using NhnToast.Sms.Sender.Enums;

namespace NhnToast.Sms.Sender.Models
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