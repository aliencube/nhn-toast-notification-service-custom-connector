namespace NhnToastSms.FunctionApp.Models
{
    public class UploadDocumentResponseModel : BaseResponseModel<ResponseItemBody<FileResponse>>
    {
    }

    public class FileResponse
    {
        public virtual int FileId { get; set; }
        public virtual string FileName { get; set; }
        public virtual string FilePath { get; set; }
    }
}