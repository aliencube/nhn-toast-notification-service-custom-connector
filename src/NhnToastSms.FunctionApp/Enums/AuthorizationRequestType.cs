using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NhnToastSms.FunctionApp.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuthorizationRequestType
    {
        [EnumMember(Value = "SMS_AUTH")]
        SmsAuth = 1,

        [EnumMember(Value = "DOCUMENT_AUTH")]
        DocumentAuth = 2,

        [EnumMember(Value = "REGIST_AUTH")]
        ManualAuth = 3,
    }
}