using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Aliencube.AzureFunctions.Extensions.Common;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using NhnToast.Sms.Verification.ActionResults;
using NhnToast.Sms.Verification.Builders;
using NhnToast.Sms.Verification.Configurations;
using NhnToast.Sms.Verification.Enums;
using NhnToast.Sms.Verification.Examples;
using NhnToast.Sms.Verification.Exceptions;
using NhnToast.Sms.Verification.Models;
using NhnToast.Sms.Verification.Validators;

namespace NhnToast.Sms.Verification.Triggers
{
    public class SenderHttpTrigger
    {
        private readonly ToastSettings _settings;
        private readonly HttpClient _http;
        private readonly ILogger _logger;

        public SenderHttpTrigger(ToastSettings settings, IHttpClientFactory factory, ILogger<SenderHttpTrigger> logger)
        {
            this._settings = settings.ThrowIfNullOrDefault();
            this._http = factory.ThrowIfNullOrDefault().CreateClient("sender");
            this._logger = logger.ThrowIfNullOrDefault();
        }

        [FunctionName(nameof(SenderHttpTrigger.UploadDocumentForAuthorization))]
        [OpenApiOperation(operationId: "Sender.Authorization.UploadDocument", tags: new[] { "sender" }, Summary = "Uploads a document for authorization", Description = "This uploads a document to get the sender's phone number authorized", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity(schemeName: "app_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-app-key", In = OpenApiSecurityLocationType.Header, Description = "Unique application key")]
        [OpenApiSecurity(schemeName: "secret_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-secret-key", In = OpenApiSecurityLocationType.Header, Description = "Unique secret key")]
        [OpenApiRequestBody(contentType: ContentTypes.MultipartFormData, bodyType: typeof(UploadDocumentRequestModel), Required = true, Description = "Document for authorization")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: ContentTypes.ApplicationJson, bodyType: typeof(UploadDocumentResponseModel), Example = typeof(UploadDocumentResponseSuccessExample), Summary = "Represents the successful operation", Description = "This represents the successful operation")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: ContentTypes.ApplicationJson, bodyType: typeof(UploadDocumentResponseModel), Example = typeof(UploadDocumentResponseFailureExample), Summary = "Represents the document upload failure", Description = "This represents the document upload failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Represents the invalid request failure", Description = "This represents the invalid request failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Unauthorized, Summary = "Represents the unauthorised request failure", Description = "This represents the unauthorised request failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "Represents the not found request failure", Description = "This represents the not found request failure")]
        public async Task<IActionResult> UploadDocumentForAuthorization(
            [HttpTrigger(AuthorizationLevel.Anonymous, HttpVerbs.POST, Route = "upload-document")] HttpRequest req)
        {
            var headers = default(RequestHeaderModel);
            try
            {
                headers = await req.To<RequestHeaderModel>(SourceFrom.Header).Validate().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // var error = new RequestErrorResponseModel(ex);
                // return new BadRequestObjectResult(error);
                var statusCode = ex is ToastException e ? (int)e.StatusCode : (int)HttpStatusCode.BadRequest;
                return new StatusCodeResult(statusCode);
            }

            var content = await req.ToMultipartFormDataContent().ConfigureAwait(false);

            var requestUrl = new RequestUrlBuilder(this._settings)
                                 .WithApiUrl(ApiUrls.UploadDocumentForAuthorizationUrl)
                                 .Build(new Dictionary<string, object>() { { "{appKey}", headers.AppKey } });

            var res = default(UploadDocumentResponseModel);
            this._http.DefaultRequestHeaders.Add(ApiHeaders.SecretHeaderKey, headers.SecretKey);
            using (var msg = await this._http.PostAsync(requestUrl, content).ConfigureAwait(false))
            {
                res = await msg.Content.ReadAsAsync<UploadDocumentResponseModel>().ConfigureAwait(false);
            }

            var result = default(IActionResult);
            if (res.Header.IsSuccessful)
            {
                result = new OkObjectResult(res);
            }
            else
            {
                result = new InternalServerErrorObjectResult(res);
            }

            return result;
        }

        [FunctionName(nameof(SenderHttpTrigger.RequestForAuthorization))]
        [OpenApiOperation(operationId: "Sender.Authorization.Request", tags: new[] { "sender" }, Summary = "Requests authorization", Description = "This requests authorization for the sender's phone numbers", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity(schemeName: "app_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-app-key", In = OpenApiSecurityLocationType.Header, Description = "Unique application key")]
        [OpenApiSecurity(schemeName: "secret_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-secret-key", In = OpenApiSecurityLocationType.Header, Description = "Unique secret key")]
        [OpenApiRequestBody(contentType: ContentTypes.ApplicationJson, bodyType: typeof(RequestAuthorizationRequestModel), Required = true, Description = "Sender's phone numbers for authorization")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: ContentTypes.ApplicationJson, bodyType: typeof(RequestAuthorizationResponseModel), Example = typeof(RequestAuthorizationResponseModelSuccessExample), Summary = "Represents the successful operation", Description = "This represents the successful operation")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: ContentTypes.ApplicationJson, bodyType: typeof(RequestAuthorizationResponseModel), Example = typeof(RequestAuthorizationResponseModelFailureExample), Summary = "Represents the request failure", Description = "This represents the request failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Represents the invalid request failure", Description = "This represents the invalid request failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Unauthorized, Summary = "Represents the unauthorised request failure", Description = "This represents the unauthorised request failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "Represents the not found request failure", Description = "This represents the not found request failure")]
        public async Task<IActionResult> RequestForAuthorization(
            [HttpTrigger(AuthorizationLevel.Anonymous, HttpVerbs.POST, Route = "request")] HttpRequest req)
        {
            var headers = default(RequestHeaderModel);
            try
            {
                headers = await req.To<RequestHeaderModel>(SourceFrom.Header).Validate().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // var error = new RequestErrorResponseModel(ex);
                // return new BadRequestObjectResult(error);
                var statusCode = ex is ToastException e ? (int)e.StatusCode : (int)HttpStatusCode.BadRequest;
                return new StatusCodeResult(statusCode);
            }

            var payload = await req.To<RequestAuthorizationRequestModel>(SourceFrom.Body).ConfigureAwait(false);

            var requestUrl = new RequestUrlBuilder(this._settings)
                                 .WithApiUrl(ApiUrls.RequestForAuthorizationUrl)
                                 .Build(new Dictionary<string, object>() { { "{appKey}", headers.AppKey } });

            var res = default(RequestAuthorizationResponseModel);
            this._http.DefaultRequestHeaders.Add(ApiHeaders.SecretHeaderKey, headers.SecretKey);
            using (var msg = await this._http.PostAsJsonAsync(requestUrl, payload).ConfigureAwait(false))
            {
                res = await msg.Content.ReadAsAsync<RequestAuthorizationResponseModel>().ConfigureAwait(false);
            }

            var result = default(IActionResult);
            if (res.Header.IsSuccessful)
            {
                result = new OkObjectResult(res);
            }
            else
            {
                result = new InternalServerErrorObjectResult(res);
            }

            return result;
        }

        [FunctionName(nameof(SenderHttpTrigger.GetAuthorizationStatus))]
        [OpenApiOperation(operationId: "Sender.Authorization.Status", tags: new[] { "sender" }, Summary = "Gets authorization status", Description = "This gets authorization status for the sender's phone numbers", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity(schemeName: "app_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-app-key", In = OpenApiSecurityLocationType.Header, Description = "Unique application key")]
        [OpenApiSecurity(schemeName: "secret_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-secret-key", In = OpenApiSecurityLocationType.Header, Description = "Unique secret key")]
        [OpenApiParameter(name: "sendNo", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The sender's phone number requested")]
        [OpenApiParameter(name: "status", In = ParameterLocation.Query, Required = false, Type = typeof(AuthorizationStatusType), Description = "The authorization status")]
        [OpenApiParameter(name: "pageNum", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Page number (default to 1)")]
        [OpenApiParameter(name: "pageSize", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Number of items in a page (default to 15)")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: ContentTypes.ApplicationJson, bodyType: typeof(GetAuthorizationStatusResponseModel), Example = typeof(GetAuthorizationStatusResponseModelSuccessExample), Summary = "Represents the successful operation", Description = "This represents the successful operation")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: ContentTypes.ApplicationJson, bodyType: typeof(GetAuthorizationStatusResponseModel), Example = typeof(GetAuthorizationStatusResponseModelFailureExample), Summary = "Represents the request failure", Description = "This represents the request failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Represents the invalid request failure", Description = "This represents the invalid request failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Unauthorized, Summary = "Represents the unauthorised request failure", Description = "This represents the unauthorised request failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "Represents the not found request failure", Description = "This represents the not found request failure")]
        public async Task<IActionResult> GetAuthorizationStatus(
            [HttpTrigger(AuthorizationLevel.Anonymous, HttpVerbs.GET, Route = "request")] HttpRequest req)
        {
            var headers = default(RequestHeaderModel);
            try
            {
                headers = await req.To<RequestHeaderModel>(SourceFrom.Header).Validate().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // var error = new RequestErrorResponseModel(ex);
                // return new BadRequestObjectResult(error);
                var statusCode = ex is ToastException e ? (int)e.StatusCode : (int)HttpStatusCode.BadRequest;
                return new StatusCodeResult(statusCode);
            }

            var queries = await req.To<GetAuthorizationStatusRequestModel>(SourceFrom.Query).ConfigureAwait(false);

            var requestUrl = new RequestUrlBuilder(this._settings)
                                 .WithApiUrl(ApiUrls.GetAuthorizationStatusUrl)
                                 .WithQueries(queries)
                                 .Build(new Dictionary<string, object>() { { "{appKey}", headers.AppKey } });

            var res = default(GetAuthorizationStatusResponseModel);
            this._http.DefaultRequestHeaders.Add(ApiHeaders.SecretHeaderKey, headers.SecretKey);
            using (var msg = await this._http.GetAsync(requestUrl).ConfigureAwait(false))
            {
                res = await msg.Content.ReadAsAsync<GetAuthorizationStatusResponseModel>().ConfigureAwait(false);
            }

            var result = default(IActionResult);
            if (res.Header.IsSuccessful)
            {
                result = new OkObjectResult(res);
            }
            else
            {
                result = new InternalServerErrorObjectResult(res);
            }

            return result;
        }

        [FunctionName(nameof(SenderHttpTrigger.GetSenderNumbers))]
        [OpenApiOperation(operationId: "Sender.GetNumbers", tags: new[] { "sender" }, Summary = "Gets the sender's phone numbers", Description = "This gets the sender's phone numbers", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity(schemeName: "app_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-app-key", In = OpenApiSecurityLocationType.Header, Description = "Unique application key")]
        [OpenApiSecurity(schemeName: "secret_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-secret-key", In = OpenApiSecurityLocationType.Header, Description = "Unique secret key")]
        [OpenApiParameter(name: "sendNo", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The sender's phone number requested")]
        [OpenApiParameter(name: "useYn", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The value indicating whether the number is used or not")]
        [OpenApiParameter(name: "blockYn", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The value indicating whether the number is blocked or not")]
        [OpenApiParameter(name: "pageNum", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Page number (default to 1)")]
        [OpenApiParameter(name: "pageSize", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Number of items in a page (default to 15)")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: ContentTypes.ApplicationJson, bodyType: typeof(GetSenderNumbersResponseModel), Example = typeof(GetSenderNumbersResponseModelSuccessExample), Summary = "Represents the successful operation", Description = "This represents the successful operation")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: ContentTypes.ApplicationJson, bodyType: typeof(GetSenderNumbersResponseModel), Example = typeof(GetSenderNumbersResponseModelFailureExample), Summary = "Represents the request failure", Description = "This represents the request failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Summary = "Represents the invalid request failure", Description = "This represents the invalid request failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Unauthorized, Summary = "Represents the unauthorised request failure", Description = "This represents the unauthorised request failure")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = "Represents the not found request failure", Description = "This represents the not found request failure")]
        public async Task<IActionResult> GetSenderNumbers(
            [HttpTrigger(AuthorizationLevel.Anonymous, HttpVerbs.GET, Route = "numbers")] HttpRequest req)
        {
            var headers = default(RequestHeaderModel);
            try
            {
                headers = await req.To<RequestHeaderModel>(SourceFrom.Header).Validate().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // var error = new RequestErrorResponseModel(ex);
                // return new BadRequestObjectResult(error);
                var statusCode = ex is ToastException e ? (int)e.StatusCode : (int)HttpStatusCode.BadRequest;
                return new StatusCodeResult(statusCode);
            }

            var queries = await req.To<GetSenderNumbersRequestModel>(SourceFrom.Query).ConfigureAwait(false);

            var requestUrl = new RequestUrlBuilder(this._settings)
                                 .WithApiUrl(ApiUrls.GetSenderNumbersUrl)
                                 .WithQueries(queries)
                                 .Build(new Dictionary<string, object>() { { "{appKey}", headers.AppKey } });

            var res = default(GetSenderNumbersResponseModel);
            this._http.DefaultRequestHeaders.Add(ApiHeaders.SecretHeaderKey, headers.SecretKey);
            using (var msg = await this._http.GetAsync(requestUrl).ConfigureAwait(false))
            {
                res = await msg.Content.ReadAsAsync<GetSenderNumbersResponseModel>().ConfigureAwait(false);
            }

            var result = default(IActionResult);
            if (res.Header.IsSuccessful)
            {
                result = new OkObjectResult(res);
            }
            else
            {
                result = new InternalServerErrorObjectResult(res);
            }

            return result;
        }
    }
}