using FMFT.Web.Server.Services.Foundations.Shows;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using FMFT.Web.Server.Models.Shows.Params;
using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Services.Orchestrations.AccountShows;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowsController : RESTFulController
    {
        private readonly IAccountShowOrchestrationService accountShowService;

        public ShowsController(IAccountShowOrchestrationService accountShowService)
        {
            this.accountShowService = accountShowService;
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
            } catch (ShowNotFoundException)
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
            } catch (AuditoriumNotExistsException exception)
            {
                return Conflict(exception);
            } catch (AddShowValidationException exception)
            {
                return BadRequest(exception);
            } catch (AccountNotAuthorizedException exception)
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
            catch (AuditoriumNotExistsException exception)
            {
                return Conflict(exception);
            } catch (ShowNotFoundException exception)
            {
                return NotFound(exception);
            } catch (UpdateShowValidationException exception)
            {
                return BadRequest(exception);
            } catch (AccountNotAuthorizedException exception)
            {
                return Unauthorized(exception);
            }
        }
    }
}
