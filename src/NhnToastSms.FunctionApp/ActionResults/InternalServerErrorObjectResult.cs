using System.Net;

using Microsoft.AspNetCore.Mvc;

namespace NhnToastSms.FunctionApp.ActionResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            this.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}