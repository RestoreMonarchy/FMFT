using FMFT.Web.Server.Services.Foundations.Shows;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using FMFT.Web.Server.Models.Shows.Params;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowsController : RESTFulController
    {
        private readonly IShowService showService;

        public ShowsController(IShowService showService)
        {
            this.showService = showService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetShows()
        {
            IEnumerable<Show> shows = await showService.RetrieveAllShowsAsync();
            return Ok(shows);
        }

        [HttpGet("{showId}")]
        public async ValueTask<IActionResult> GetShow(int showId)
        {
            try
            {
                Show show = await showService.RetrieveShowByIdAsync(showId);
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
                Show show = await showService.AddShowAsync(@params);
                return Ok(show);
            } catch (AuditoriumNotExistsException)
            {
                return Conflict();
            } catch (AddShowValidationException exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPut]
        public async ValueTask<IActionResult> PutShow([FromBody] UpdateShowParams @params)
        {
            try
            {
                Show show = await showService.ModifyShowAsync(@params);
                return Ok(show);
            }
            catch (AuditoriumNotExistsException)
            {
                return Conflict();
            } catch (ShowNotFoundException)
            {
                return NotFound();
            } catch (UpdateShowValidationException exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
