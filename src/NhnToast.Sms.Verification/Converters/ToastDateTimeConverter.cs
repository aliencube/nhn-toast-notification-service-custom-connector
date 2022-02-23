using Newtonsoft.Json.Converters;

namespace NhnToast.Sms.Verification.Converters
{
    public class ToastDateTimeConverter : IsoDateTimeConverter
    {
        public ToastDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}