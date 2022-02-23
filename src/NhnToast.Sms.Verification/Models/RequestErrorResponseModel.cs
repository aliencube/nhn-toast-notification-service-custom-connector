using System;
using System.Net;

using NhnToast.Sms.Verification.Exceptions;

namespace NhnToast.Sms.Verification.Models
{
    public class RequestErrorResponseModel : BaseResponseModel<object>
    {
        public RequestErrorResponseModel(Exception ex)
        {
            this.Header = new ResponseHeader()
            {
                IsSuccessful = false,
                ResultCode = ex is ToastException e ? (int)e.StatusCode : (int)HttpStatusCode.BadRequest,
                ResultMessage = ex.Message
            };
        }
    }
}