using System.Net;

using Microsoft.AspNetCore.Mvc;

namespace NhnToast.Sms.Sender.ActionResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            this.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}