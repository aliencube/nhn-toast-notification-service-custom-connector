using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NhnToastSms.FunctionApp.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuthorizationStatusType
    {
        [EnumMember(Value = "SRS01")]
        AuthorizationRequested = 1,

        [EnumMember(Value = "SRS02")]
        Reviewing = 2,

        [EnumMember(Value = "SRS03")]
        AuthorizationCompleted = 3,

        [EnumMember(Value = "SRS04")]
        AuthorizationRejected = 4,

        [EnumMember(Value = "SRS05")]
        PhoneBeingCertified = 5,

        [EnumMember(Value = "SRS06")]
        PhoneCertifyingFailed = 6,

        [EnumMember(Value = "SRS07")]
        ManualAuthorizationCompleted = 7,
    }
}