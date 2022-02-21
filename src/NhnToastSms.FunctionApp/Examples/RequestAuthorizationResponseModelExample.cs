using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;

using Newtonsoft.Json.Serialization;

using NhnToastSms.FunctionApp.Models;

namespace NhnToastSms.FunctionApp.Examples
{
    public class RequestAuthorizationResponseModelSuccessExample : OpenApiExample<RequestAuthorizationResponseModel>
    {
        public override IOpenApiExample<RequestAuthorizationResponseModel> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve("success",
                new RequestAuthorizationResponseModel()
                {
                    Header = new ResponseHeader()
                    {
                        IsSuccessful = true,
                        Resultcode = 0,
                        ResultMessage = "SUCCESS"
                    },
                    Body = null,
                },
                namingStrategy));

            return this;
        }
    }

    public class RequestAuthorizationResponseModelFailureExample : OpenApiExample<RequestAuthorizationResponseModel>
    {
        public override IOpenApiExample<RequestAuthorizationResponseModel> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve("invalid",
                new RequestAuthorizationResponseModel()
                {
                    Header = new ResponseHeader()
                    {
                        IsSuccessful = false,
                        Resultcode = -2314,
                        ResultMessage = "This sendno is invalid."
                    },
                    Body = null,
                },
                namingStrategy));

            this.Examples.Add(
                OpenApiExampleResolver.Resolve("duplicated",
                new RequestAuthorizationResponseModel()
                {
                    Header = new ResponseHeader()
                    {
                        IsSuccessful = false,
                        Resultcode = -2301,
                        ResultMessage = "Already regist send no."
                    },
                    Body = null,
                },
                namingStrategy));

            return this;
        }
    }
}