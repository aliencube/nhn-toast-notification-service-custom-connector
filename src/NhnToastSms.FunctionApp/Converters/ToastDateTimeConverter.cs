using Newtonsoft.Json.Converters;

namespace NhnToastSms.FunctionApp.Converters
{
    public class ToastDateTimeConverter : IsoDateTimeConverter
    {
        public ToastDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}