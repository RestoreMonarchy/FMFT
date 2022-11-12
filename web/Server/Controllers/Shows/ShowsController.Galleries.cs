using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.ShowGalleries;
using FMFT.Web.Server.Models.ShowGalleries.Params;
using FMFT.Web.Server.Models.Shows.Params;
using Microsoft.AspNetCore.Mvc;

namespace FMFT.Web.Server.Controllers.Shows
{
    public partial class ShowsController
    {
        [HttpGet("{showId}/gallery")]
        public async ValueTask<IActionResult> GetShowGallery(int showId)
        {
            IEnumerable<ShowGallery> showGalleries = await showGalleryCoordinationService.RetrieveShowGalleryByShowIdAsync(showId);

            return Ok(showGalleries);
        }

        [HttpPost("{showId}/gallery/add")]
        public async ValueTask<IActionResult> AddShowGallery(int showId, AddShowGalleryParams @params)
        {
            try
            {
                @params.ShowId = showId;
                ShowGallery showGallery = await showGalleryCoordinationService.AddShowGalleryAsync(@params);

                return Ok(showGallery);
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            } 
        }

        [HttpDelete("gallery/{showGalleryId}/delete")]
        public async ValueTask<IActionResult> DeleteShowGallery(int showGalleryId)
        {
            try
            {
                await showGalleryCoordinationService.DeleteShowGalleryByIdAsync(showGalleryId);

                return Ok();
            } catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            } catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            }
        }
    }
}
