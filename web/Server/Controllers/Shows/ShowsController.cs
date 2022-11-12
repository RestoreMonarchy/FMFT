using FMFT.Web.Server.Services.Foundations.Shows;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using FMFT.Web.Server.Models.Shows.Params;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Services.Orchestrations.Shows;
using FMFT.Web.Server.Services.Orchestrations.Reservations;
using FMFT.Web.Server.Services.Coordinations.Reservations;
using FMFT.Web.Server.Services.Coordinations.ShowGalleries;

namespace FMFT.Web.Server.Controllers.Shows
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class ShowsController : RESTFulController
    {
        private readonly IShowOrchestrationService showOrchestrationService;
        private readonly IReservationCoordinationService reservationCoordinationService;
        private readonly IShowGalleryCoordinationService showGalleryCoordinationService;

        public ShowsController(
            IShowOrchestrationService showOrchestrationService,
            IReservationCoordinationService reservationCoordinationService,
            IShowGalleryCoordinationService showGalleryCoordinationService)
        {
            this.showOrchestrationService = showOrchestrationService;
            this.reservationCoordinationService = reservationCoordinationService;
            this.showGalleryCoordinationService = showGalleryCoordinationService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetShows()
        {
            IEnumerable<Show> shows = await showOrchestrationService.RetrieveAllShowsAsync();

            return Ok(shows);
        }

        [HttpGet("{showId}")]
        public async ValueTask<IActionResult> GetShow(int showId)
        {
            try
            {
                Show show = await showOrchestrationService.RetrieveShowByIdAsync(showId);

                return Ok(show);
            }
            catch (NotFoundShowException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostShow([FromBody] AddShowParams @params)
        {
            try
            {
                Show show = await showOrchestrationService.AddShowAsync(@params);

                return Ok(show);
            }
            catch (AuditoriumNotExistsShowException exception)
            {
                return Conflict(exception);
            }
            catch (AddShowValidationException exception)
            {
                return BadRequest(exception);
            }
            catch (NotAuthorizedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpPut]
        public async ValueTask<IActionResult> PutShow([FromBody] UpdateShowParams @params)
        {
            try
            {
                Show show = await showOrchestrationService.ModifyShowAsync(@params);

                return Ok(show);
            }
            catch (AuditoriumNotExistsShowException exception)
            {
                return Conflict(exception);
            }
            catch (NotFoundShowException exception)
            {
                return NotFound(exception);
            }
            catch (UpdateShowValidationException exception)
            {
                return BadRequest(exception);
            }
            catch (NotAuthorizedAccountException exception)
            {
                return Unauthorized(exception);
            }
        }
    }
}
