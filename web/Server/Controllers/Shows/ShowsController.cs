using FMFT.Web.Server.Services.Foundations.Shows;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using FMFT.Web.Server.Models.Shows.Params;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Services.Orchestrations.AccountShows;
using FMFT.Web.Server.Services.Orchestrations.AccountReservations;

namespace FMFT.Web.Server.Controllers.Shows
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class ShowsController : RESTFulController
    {
        private readonly IAccountShowOrchestrationService accountShowService;
        private readonly IAccountReservationOrchestrationService accountReservationService;

        public ShowsController(IAccountShowOrchestrationService accountShowService, IAccountReservationOrchestrationService accountReservationService)
        {
            this.accountShowService = accountShowService;
            this.accountReservationService = accountReservationService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetShows()
        {
            IEnumerable<Show> shows = await accountShowService.RetrieveAllShowsAsync();
            return Ok(shows);
        }

        [HttpGet("{showId}")]
        public async ValueTask<IActionResult> GetShow(int showId)
        {
            try
            {
                Show show = await accountShowService.RetrieveShowByIdAsync(showId);
                return Ok(show);
            }
            catch (ShowNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostShow([FromBody] AddShowParams @params)
        {
            try
            {
                Show show = await accountShowService.AddShowAsync(@params);
                return Ok(show);
            }
            catch (ShowAuditoriumNotExistsException exception)
            {
                return Conflict(exception);
            }
            catch (AddShowValidationException exception)
            {
                return BadRequest(exception);
            }
            catch (AccountNotAuthorizedException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpPut]
        public async ValueTask<IActionResult> PutShow([FromBody] UpdateShowParams @params)
        {
            try
            {
                Show show = await accountShowService.ModifyShowAsync(@params);
                return Ok(show);
            }
            catch (ShowAuditoriumNotExistsException exception)
            {
                return Conflict(exception);
            }
            catch (ShowNotFoundException exception)
            {
                return NotFound(exception);
            }
            catch (UpdateShowValidationException exception)
            {
                return BadRequest(exception);
            }
            catch (AccountNotAuthorizedException exception)
            {
                return Unauthorized(exception);
            }
        }
    }
}
