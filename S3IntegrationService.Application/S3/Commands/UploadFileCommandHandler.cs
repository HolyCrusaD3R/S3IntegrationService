using MediatR;
using S3IntegrationService.Application.FileStorage;

namespace S3IntegrationService.Application.S3.Commands
{
    public class UploadFileCommand : IRequest
    {
        public string FolderName { get; init; } = null!;
        public string FileName { get; init; } = null!;
        public string ContentType { get; init; } = null!;
        public Stream FileStream { get; init; } = null!;
    }
    public class UploadFileCommandHandler(IFileStorageService fileStorageService) : IRequestHandler<UploadFileCommand>
    {
        public async Task Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            await fileStorageService.SaveFileAsync(new FileDetails(
                request.FileName,
                request.FolderName,
                request.ContentType,
                request.FileStream), cancellationToken).ConfigureAwait(false);
        }
    }
}
