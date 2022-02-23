using System;
using System.Net;
using System.Runtime.Serialization;

namespace NhnToast.Sms.Verification.Exceptions
{
    public class RequestHeaderNotValidException : ToastException
    {
        public RequestHeaderNotValidException()
        {
        }

        public RequestHeaderNotValidException(string message)
            : base(message)
        {
        }

        public RequestHeaderNotValidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected RequestHeaderNotValidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}