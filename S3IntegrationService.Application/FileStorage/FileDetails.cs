namespace S3IntegrationService.Application.FileStorage
{
    public class FileDetails
    {
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string ContentType { get; set; }
        public Stream FileStream { get; set; }

        public FileDetails(string fileName, string folderName, string contentType, Stream fileStream)
        {
            FileName = fileName;
            FolderName = folderName;
            ContentType = contentType;
            FileStream = fileStream;
        }

        public List<string> GetValidationErrors()
        {
            List<string> errors = new List<string>();

            if(string.IsNullOrWhiteSpace(FileName))
                errors.Add($"{nameof(FileName)} cannot be null, empty, or whitespace.");

            if(string.IsNullOrWhiteSpace(FolderName))
                errors.Add($"{nameof(FolderName)} cannot be null, empty, or whitespace.");

            if(FileStream == null)
                errors.Add($"{nameof(FileStream)} cannot be null.");
            
            if(!FileStream?.CanRead ?? false)
                errors.Add($"{nameof(FileStream)} must be readable.");

            return errors;
        }
    }
}
