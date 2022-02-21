using System;

using Newtonsoft.Json;

using NhnToastSms.FunctionApp.Converters;

namespace NhnToastSms.FunctionApp.Models
{
    public class GetSenderNumbersResponseModel : BaseResponseModel<ResponseCollectionBody<SenderNumbersResponse>>
    {
    }

    public class SenderNumbersResponse
    {
        public virtual int ServiceId { get; set; }

        [JsonProperty("sendNo")]
        public virtual string SenderNumber { get; set; }

        [JsonProperty("useYn")]
        public virtual string UseNumber { get; set; }

        [JsonProperty("blockYn")]
        public virtual string BlockedNumber { get; set; }

        [JsonProperty("blockReason")]
        public virtual string BlockedReason { get; set; }

        [JsonConverter(typeof(ToastDateTimeConverter))]
        public virtual DateTimeOffset CreateDate { get; set; }

        public virtual string CreateUser { get; set; }

        [JsonConverter(typeof(ToastDateTimeConverter))]
        public virtual DateTimeOffset UpdateDate { get; set; }

        public virtual string UpdateUser { get; set; }
    }
}