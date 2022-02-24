using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;

using Newtonsoft.Json.Serialization;

using NhnToast.Sms.Verification.Exceptions;
using NhnToast.Sms.Verification.Models;

namespace NhnToast.Sms.Verification.Examples
{
    public class RequestErrorResponseModelExample : OpenApiExample<RequestErrorResponseModel>
    {
        public override IOpenApiExample<RequestErrorResponseModel> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve("badrequest",
                new RequestErrorResponseModel(new RequestHeaderNotValidException("Header Not Valid")),
                namingStrategy)); ;

            this.Examples.Add(
                OpenApiExampleResolver.Resolve("notfound",
                new RequestErrorResponseModel(new RequestHeaderNotValidException("Not Found")),
                namingStrategy)); ;

            this.Examples.Add(
                OpenApiExampleResolver.Resolve("unauthorized",
                new RequestErrorResponseModel(new RequestHeaderNotValidException("Unauthorized")),
                namingStrategy)); ;

            return this;
        }
    }
}