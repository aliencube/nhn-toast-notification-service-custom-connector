using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;

using Newtonsoft.Json.Serialization;

using NhnToastSms.FunctionApp.Exceptions;
using NhnToastSms.FunctionApp.Models;

namespace NhnToastSms.FunctionApp.Examples
{
    public class RequestErrorResponseModelExample : OpenApiExample<RequestErrorResponseModel>
    {
        public override IOpenApiExample<RequestErrorResponseModel> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve("badrequest",
                new RequestErrorResponseModel(new RequestHeaderNotValidException("Header not valid")),
                namingStrategy)); ;

            return this;
        }
    }
}