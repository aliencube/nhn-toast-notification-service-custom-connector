using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using NhnToast.Sms.Verification.Converters;
using NhnToast.Sms.Verification.Enums;

namespace NhnToast.Sms.Verification.Models
{
    public class GetAuthorizationStatusResponseModel : BaseResponseModel<ResponseCollectionBody<AuthorizationStatusResponse>>
    {
    }

    public class AuthorizationStatusResponse
    {
        public virtual AuthorizationRequestType AuthType { get; set; }

        [JsonProperty("sendNos")]
        public virtual List<string> SenderNumbers { get; set; } = new List<string>();

        public virtual string Comment { get; set; }

        public virtual string RequestInfo { get; set; }

        public virtual List<int> FileIds { get; set; }

        public virtual AuthorizationRequestStatusType Status { get; set; }

        public virtual string StatusName { get; set; }

        [JsonConverter(typeof(ToastDateTimeConverter))]
        public virtual DateTimeOffset? CreateDate { get; set; }

        [JsonConverter(typeof(ToastDateTimeConverter))]
        public virtual DateTimeOffset? UpdateDate { get; set; }

        [JsonConverter(typeof(ToastDateTimeConverter))]
        public virtual DateTimeOffset? ConfirmDate { get; set; }

        public virtual List<object> AttachFileList { get; set; }
    }
}