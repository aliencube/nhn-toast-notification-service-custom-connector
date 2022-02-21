using System;
using System.Collections.Generic;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;

using Newtonsoft.Json.Serialization;

using NhnToastSms.FunctionApp.Models;

namespace NhnToastSms.FunctionApp.Examples
{
    public class GetSenderNumbersResponseModelSuccessExample : OpenApiExample<GetSenderNumbersResponseModel>
    {
        public override IOpenApiExample<GetSenderNumbersResponseModel> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve("success",
                new GetSenderNumbersResponseModel()
                {
                    Header = new ResponseHeader()
                    {
                        IsSuccessful = true,
                        Resultcode = 0,
                        ResultMessage = "SUCCESS"
                    },
                    Body = new ResponseCollectionBody<SenderNumbersResponse>()
                    {
                        PageNumber = 1,
                        PageSize = 15,
                        TotalCount = 1,
                        Data = new List<SenderNumbersResponse>()
                                      {
                                          new SenderNumbersResponse()
                                          {
                                              ServiceId = 1,
                                              SenderNumber = "01011111111",
                                              UseNumber = "Y",
                                              BlockedNumber = "N",
                                              BlockedReason = "N",
                                              CreateDate = new DateTimeOffset(2014, 4, 16, 8, 50, 0, new TimeSpan(9, 0, 0)),
                                              CreateUser = "System",
                                              UpdateDate = new DateTimeOffset(2014, 4, 16, 8, 50, 0, new TimeSpan(9, 0, 0)),
                                              UpdateUser = "System",
                                          }
                                      }
                    },
                },
                namingStrategy));

            return this;
        }
    }

    public class GetSenderNumbersResponseModelFailureExample : OpenApiExample<GetSenderNumbersResponseModel>
    {
        public override IOpenApiExample<GetSenderNumbersResponseModel> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve("notfound",
                new GetSenderNumbersResponseModel()
                {
                    Header = new ResponseHeader()
                    {
                        IsSuccessful = false,
                        Resultcode = -9998,
                        ResultMessage = "Not found"
                    },
                    Body = null,
                },
                namingStrategy));

            return this;
        }
    }
}