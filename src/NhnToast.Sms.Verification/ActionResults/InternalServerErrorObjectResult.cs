using System.Net;

using Microsoft.AspNetCore.Mvc;

namespace NhnToast.Sms.Verification.ActionResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            this.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}