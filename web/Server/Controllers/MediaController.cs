using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Medias;
using FMFT.Web.Server.Models.Medias.Exceptions;
using FMFT.Web.Server.Services.Coordinations.Medias;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaController : RESTFulController
    {
        private readonly IMediaCoordinationService mediaCoordinationService;

        public MediaController(IMediaCoordinationService mediaCoordinationService)
        {
            this.mediaCoordinationService = mediaCoordinationService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllMedia()
        {
            try
            {
                IEnumerable<Media> media = await mediaCoordinationService.RetrieveAllMediaAsync();

                return Ok(media);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpGet("file/{mediaId}")]
        [ResponseCache(Duration = 3 * 60 * 60)]
        public async ValueTask<IActionResult> MediaFileById(Guid mediaId)
        {
            try
            {
                Media media = await mediaCoordinationService.RetrieveMediaByIdAsync(mediaId);

                Response.Headers.Add("Content-Disposition", "inline; filename=" + media.Name);

                return File(media.Content, media.ContentType);
            } catch (NotFoundMediaException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpPost("upload")]
        public async ValueTask<IActionResult> UploadMedia(IFormFile formFile)
        {
            try
            {
                Media media = await mediaCoordinationService.AddMediaFromFormFileAsync(formFile);

                return Ok(media);
            } catch (TooLargeFileMediaException exception)
            {
                return RequestEntityTooLarge(exception);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            } catch (NotSuppliedFileMediaException exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
