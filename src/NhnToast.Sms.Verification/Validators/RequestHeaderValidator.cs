using System.Net;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;

using NhnToast.Sms.Verification.Exceptions;
using NhnToast.Sms.Verification.Models;

namespace NhnToast.Sms.Verification.Validators
{
    public static class RequestHeaderValidator
    {
        public static async Task<RequestHeaderModel> Validate(this Task<RequestHeaderModel> headers)
        {
            var instance = await headers.ConfigureAwait(false);
            if (instance.AppKey.IsNullOrWhiteSpace())
            {
                throw new RequestHeaderNotValidException("Not Found") { StatusCode = HttpStatusCode.NotFound };
            }
            if (instance.SecretKey.IsNullOrWhiteSpace())
            {
                throw new RequestHeaderNotValidException("Unauthorized") { StatusCode = HttpStatusCode.Unauthorized };
            }

            return instance;
        }
    }
}