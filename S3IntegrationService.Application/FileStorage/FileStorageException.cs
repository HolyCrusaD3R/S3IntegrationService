namespace S3IntegrationService.Application.FileStorage
{
    public class FileStorageException : Exception
    {
        public FileStorageException(string message) : base(message)
        {
            
        }

        public FileStorageException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
