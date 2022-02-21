using System.Collections.Generic;
using System.Linq;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;

using Newtonsoft.Json;

using NhnToastSms.FunctionApp.Configurations;

namespace NhnToastSms.FunctionApp.Builders
{
    public class RequestUrlBuilder
    {
        private static readonly JsonSerializerSettings serialiserSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        private readonly ToastSettings _settings;

        private string _hostname;
        private string _baseUrl;
        private string _apiVersion;
        private string _apiUrl;
        private string _queries;

        public RequestUrlBuilder()
        {
        }

        public RequestUrlBuilder(ToastSettings settings)
        {
            this._settings = settings.ThrowIfNullOrDefault();

            this._hostname = this._settings.HostName;
            this._baseUrl = this._settings.BaseUrl;
            this._apiVersion = this._settings.ApiVersion;
        }

        public RequestUrlBuilder WithHostName(string hostname)
        {
            this._hostname = hostname.ThrowIfNullOrWhiteSpace();

            return this;
        }

        public RequestUrlBuilder WithBaseUrl(string baseUrl)
        {
            this._baseUrl = baseUrl.ThrowIfNullOrWhiteSpace();

            return this;
        }

        public RequestUrlBuilder WithApiVersion(string apiVersion)
        {
            this._apiVersion = apiVersion.ThrowIfNullOrWhiteSpace();

            return this;
        }

        public RequestUrlBuilder WithApiUrl(string apiUrl)
        {
            this._apiUrl = apiUrl.ThrowIfNullOrWhiteSpace();

            return this;
        }

        public RequestUrlBuilder WithQueries<T>(T queries)
        {
            var serialised = JsonConvert.SerializeObject(queries, serialiserSettings);
            var deserialised = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialised);
            if (!deserialised.Any())
            {
                return this;
            }

            this._queries = string.Join("&", deserialised.Select(p => $"{p.Key}={p.Value}"));

            return this;
        }

        public string Build(IDictionary<string, object> @params = null)
        {
            var result = $"{this._hostname.TrimEnd('/')}/{this._baseUrl.TrimEnd('/')}/{this._apiVersion.Trim('/')}/{this._apiUrl.TrimStart('/')}";
            if (!this._queries.IsNullOrWhiteSpace())
            {
                result += $"?{this._queries}";
            }

            if (@params.IsNullOrDefault())
            {
                return result;
            }

            foreach (var param in @params)
            {
                result = result.Replace(param.Key, param.Value.ToString());
            }

            return result;
        }
    }
}