namespace S3IntegrationService.Application.FileStorage
{
    public interface IFileStorageService
    {
        Task SaveFileAsync(FileDetails file, CancellationToken token = default);
    }
}
