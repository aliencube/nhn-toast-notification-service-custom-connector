using System;
using System.Net;
using Newtonsoft.Json;
using NhnToast.Sms.Verification.Exceptions;

namespace NhnToast.Sms.Verification.Models
{
    public class RequestErrorResponseModel : BaseResponseModel<object>
    {
        public RequestErrorResponseModel()
        {
        }

        public RequestErrorResponseModel(Exception ex)
        {
            this.Header = new ResponseHeader()
            {
                IsSuccessful = false,
                ResultCode = ex is ToastException e ? (int)e.StatusCode : (int)HttpStatusCode.BadRequest,
                ResultMessage = ex.Message
            };
        }

        [JsonProperty("header", NullValueHandling = NullValueHandling.Ignore)]
        public override ResponseHeader Header { get; set; }

        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public override object Body { get; set; }
    }
}