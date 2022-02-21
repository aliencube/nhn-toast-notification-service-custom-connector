using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace NhnToastSms.FunctionApp.Models
{
    public class RequestAuthorizationRequestModel
    {
        [JsonProperty("sendNos")]
        public virtual List<string> SenderNumbers { get; set; } = new List<string>();

        public virtual List<int> FileIds { get; set; } = new List<int>();

        [MaxLength(4000)]
        public virtual string Comment { get; set; }
    }
}