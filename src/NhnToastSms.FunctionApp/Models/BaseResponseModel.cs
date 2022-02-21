using Newtonsoft.Json;

using System.Collections.Generic;

namespace NhnToastSms.FunctionApp.Models
{
    public abstract class BaseResponseModel<T>
    {
        public virtual ResponseHeader Header { get; set; }
        public virtual T Body { get; set; }
    }

    public class ResponseHeader
    {
        public virtual bool IsSuccessful { get; set; }
        public virtual int Resultcode { get; set; }
        public virtual string ResultMessage { get; set; }
    }

    public class ResponseItemBody<T>
    {
        public virtual T Data { get; set; }
    }

    public class ResponseCollectionBody<T>
    {
        [JsonProperty("pageNum")]
        public virtual int PageNumber { get; set; }

        public virtual int PageSize { get; set; }

        public virtual int TotalCount { get; set; }

        public virtual List<T> Data { get; set; }
    }
}