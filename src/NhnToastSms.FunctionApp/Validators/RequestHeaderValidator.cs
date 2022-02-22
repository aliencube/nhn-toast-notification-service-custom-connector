using System.Threading.Tasks;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;

using NhnToastSms.FunctionApp.Exceptions;
using NhnToastSms.FunctionApp.Models;

namespace NhnToastSms.FunctionApp.Validators
{
    public static class RequestHeaderValidator
    {
        public static async Task<RequestHeaderModel> Validate(this Task<RequestHeaderModel> headers)
        {
            var instance = await headers.ConfigureAwait(false);
            if (instance.AppKey.IsNullOrWhiteSpace())
            {
                throw new RequestHeaderNotValidException("Header not valid");
            }
            if (instance.SecretKey.IsNullOrWhiteSpace())
            {
                throw new RequestHeaderNotValidException("Header not valid");
            }

            return instance;
        }
    }
}