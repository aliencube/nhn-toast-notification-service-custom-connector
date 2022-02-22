using Newtonsoft.Json.Converters;

namespace NhnToast.Sms.Sender.Converters
{
    public class ToastDateTimeConverter : IsoDateTimeConverter
    {
        public ToastDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}