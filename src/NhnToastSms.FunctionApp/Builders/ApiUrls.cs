namespace NhnToastSms.FunctionApp.Builders
{
    public class ApiUrls
    {
        public const string UploadDocumentForAuthorizationUrl = "/appKeys/{appKey}/requests/attachFiles/authDocuments";
        public const string RequestForAuthorizationUrl = "/appKeys/{appKey}/reqeusts/sendNos";
        public const string GetAuthorizationStatusUrl = "/appKeys/{appKey}/reqeusts/sendNos";
        public const string GetSenderNumbersUrl = "/appKeys/{appKey}/sendNos";
    }
}