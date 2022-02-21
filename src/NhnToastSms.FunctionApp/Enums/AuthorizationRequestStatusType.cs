using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NhnToastSms.FunctionApp.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuthorizationRequestStatusType
    {
        [EnumMember(Value = "REGIST_REQUEST")]
        AuthorizationRequested = 1,

        [EnumMember(Value = "EXAMINE")]
        Reviewing = 2,

        [EnumMember(Value = "COMPLETE")]
        AuthorizationCompleted = 3,

        [EnumMember(Value = "REJECT")]
        AuthorizationRejected = 4,

        [EnumMember(Value = "CERTIFYING")]
        PhoneBeingCertified = 5,

        [EnumMember(Value = "CERTIFY_FAILED")]
        PhoneCertifyingFailed = 6,

        [EnumMember(Value = "MANUAL_REGIST")]
        ManualAuthorizationCompleted = 7,
    }
}