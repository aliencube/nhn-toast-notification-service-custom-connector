using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;

using Newtonsoft.Json.Serialization;

using NhnToast.Sms.Sender.Exceptions;
using NhnToast.Sms.Sender.Models;

namespace NhnToast.Sms.Sender.Examples
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