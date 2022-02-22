using System;
using System.Collections.Generic;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;

using Newtonsoft.Json.Serialization;

using NhnToastSms.FunctionApp.Enums;
using NhnToastSms.FunctionApp.Models;

namespace NhnToastSms.FunctionApp.Examples
{
    public class GetAuthorizationStatusResponseModelSuccessExample : OpenApiExample<GetAuthorizationStatusResponseModel>
    {
        public override IOpenApiExample<GetAuthorizationStatusResponseModel> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve("success",
                new GetAuthorizationStatusResponseModel()
                {
                    Header = new ResponseHeader()
                    {
                        IsSuccessful = true,
                        ResultCode = 0,
                        ResultMessage = "SUCCESS"
                    },
                    Body = new ResponseCollectionBody<AuthorizationStatusResponse>()
                    {
                        PageNumber = 1,
                        PageSize = 15,
                        TotalCount = 1,
                        Data = new List<AuthorizationStatusResponse>()
                                      {
                                          new AuthorizationStatusResponse()
                                          {
                                              AuthType = AuthorizationRequestType.DocumentAuth,
                                              SenderNumbers = new List<string>() { "01011111111" },
                                              Comment = "Main phone number",
                                              FileIds = null,
                                              Status = AuthorizationRequestStatusType.AuthorizationRequested,
                                              CreateDate = new DateTimeOffset(2014, 4, 16, 8, 50, 0, new TimeSpan(9, 0, 0)),
                                              UpdateDate = new DateTimeOffset(2014, 4, 16, 8, 50, 0, new TimeSpan(9, 0, 0)),
                                              ConfirmDate = new DateTimeOffset(2014, 4, 16, 8, 50, 0, new TimeSpan(9, 0, 0)),
                                          }
                                      }
                    },
                },
                namingStrategy));

            return this;
        }
    }

    public class GetAuthorizationStatusResponseModelFailureExample : OpenApiExample<GetAuthorizationStatusResponseModel>
    {
        public override IOpenApiExample<GetAuthorizationStatusResponseModel> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve("notfound",
                new GetAuthorizationStatusResponseModel()
                {
                    Header = new ResponseHeader()
                    {
                        IsSuccessful = false,
                        ResultCode = -9998,
                        ResultMessage = "Not found"
                    },
                    Body = null,
                },
                namingStrategy));

            return this;
        }
    }
}