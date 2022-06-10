using FMFT.Web.Server.Services.Foundations.Shows;
using FMFT.Web.Shared.Models.Shows;
using FMFT.Web.Shared.Models.Shows.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowsController : ControllerBase
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
    }
}
