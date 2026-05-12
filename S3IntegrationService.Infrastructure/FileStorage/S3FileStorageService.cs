using Amazon.S3;
using Amazon.S3.Model;
using S3IntegrationService.Application.FileStorage;

namespace S3IntegrationService.Infrastructure.FileStorage
{
    public class S3FileStorageService(IAmazonS3 amazonS3, S3FileStorageOptions options) : IFileStorageService
    {
        private readonly IAmazonS3 _amazonS3 = amazonS3;
        private readonly S3FileStorageOptions _options = options;
        public async Task SaveFileAsync(FileDetails file, CancellationToken token = default)
        {
            var errors = file.GetValidationErrors();
            if (errors.Count != 0)
                throw new FileStorageException($"{nameof(FileDetails)} validation errors: {string.Join(";", errors)}");

            var key = $"{file.FolderName.Trim('/', '\\')}/{file.FileName}";

            var request = new PutObjectRequest
            {
                Key = key,
                BucketName = _options.BucketName,
                ContentType = file.ContentType,
                InputStream = file.FileStream,
                AutoCloseStream = false
            };

            var streamPosition = file.FileStream.Position;

            try
            {
                await _amazonS3.PutObjectAsync(request, token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new FileStorageException($"Cannot save file '{request.Key}' to bucket '{request.BucketName}'", ex);
            }
            finally
            {
                file.FileStream.Position = streamPosition;
            }
        }
    }
}
