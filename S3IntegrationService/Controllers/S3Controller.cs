using MediatR;
using Microsoft.AspNetCore.Mvc;
using S3IntegrationService.Application.FileStorage;
using S3IntegrationService.Application.S3.Commands;
using S3IntegrationService.Contract;

namespace S3IntegrationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class S3Controller(IMediator mediator) : ControllerBase
    {
        [HttpPost(Name = "PostFile")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromForm] UploadFIleRequest request, CancellationToken cancellationToken = default)
        {
            await using var stream = request.File.OpenReadStream();
            await mediator.Send(new UploadFileCommand
            {
                FolderName = FileStorageFolders.S3IntegrationFolder,
                FileName = request.File.FileName,
                ContentType = request.File.ContentType,
                FileStream = stream
            }, cancellationToken);
            return Ok();
        }
    }
}
