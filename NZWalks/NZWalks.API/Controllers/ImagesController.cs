using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly ILogger<ImagesController> logger;

        public ImagesController(IImageRepository imageRepository, ILogger<ImagesController> logger)
        {
            this.imageRepository = imageRepository;
            this.logger = logger;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto request)
        {
            try
            {
                validateFileUpload(request);

                if (ModelState.IsValid)
                {
                    Image imageModel = new Image
                    {
                        File = request.File,
                        FileExtension = Path.GetExtension(request.File.FileName),
                        FileSizeInBytes = request.File.Length,
                        FileName = request.FileName,
                        FileDescription = request.FileDescription
                    };

                    // use repository to upload image
                    await imageRepository.Upload(imageModel);

                    return Ok(imageModel);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        private void validateFileUpload(ImageUploadDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            // file size must be less than 10 MB
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "file size is more than 10 MB, please upload a smaller size file.");
            }
        }
    }
}
