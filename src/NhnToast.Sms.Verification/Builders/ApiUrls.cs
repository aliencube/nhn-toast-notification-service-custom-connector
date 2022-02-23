namespace NhnToast.Sms.Verification.Builders
{
    public class ApiUrls
    {
        public const string UploadDocumentForAuthorizationUrl = "/appKeys/{appKey}/requests/attachFiles/authDocuments";
        public const string RequestForAuthorizationUrl = "/appKeys/{appKey}/requests/sendNos";
        public const string GetAuthorizationStatusUrl = "/appKeys/{appKey}/requests/sendNos";
        public const string GetSenderNumbersUrl = "/appKeys/{appKey}/sendNos";
    }
}