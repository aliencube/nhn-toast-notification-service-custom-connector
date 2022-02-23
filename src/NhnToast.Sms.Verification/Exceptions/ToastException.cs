using System;
using System.Net;
using System.Runtime.Serialization;

namespace NhnToast.Sms.Verification.Exceptions
{
    public abstract class ToastException : Exception
    {
        public ToastException()
        {
        }

        public ToastException(string message)
            : base(message)
        {
        }

        public ToastException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ToastException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public virtual HttpStatusCode StatusCode { get; set; }
    }
}