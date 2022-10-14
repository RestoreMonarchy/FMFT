using FMFT.Web.Server.Services.Foundations.Auditoriums;
using FMFT.Web.Server.Models.Auditoriums;
using FMFT.Web.Server.Models.Auditoriums.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditoriumsController : RESTFulController
    {
        private readonly IAuditoriumService auditoriumService;

        public AuditoriumsController(IAuditoriumService auditoriumService)
        {
            this.auditoriumService = auditoriumService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAuditoriums()
        {
            try
            {
                IEnumerable<Auditorium> auditoriums = await auditoriumService.RetrieveAllAuditoriumsAsync();

                return Ok(auditoriums);
            }
            catch (Exception exception) when (exception is AuditoriumServiceException or AuditoriumDependencyException)
            {
                Exception innerException = exception.InnerException;

                return InternalServerError(innerException);
            }
        }

        [HttpGet("{auditoriumId}")]
        public async ValueTask<IActionResult> GetAuditorium(int auditoriumId)
        {
            try
            {
                Auditorium auditorium = await auditoriumService.RetrieveAuditoriumByIdAsync(auditoriumId);

                return Ok(auditorium);
            } catch (AuditoriumValidationException exception) when (exception.InnerException is NotFoundAuditoriumException)
            {
                Exception innerException = exception.InnerException;

                return NotFound(innerException);
            } catch (Exception exception) when (exception is AuditoriumServiceException or AuditoriumDependencyException)
            {
                Exception innerException = exception.InnerException;

                return InternalServerError(innerException);
            }
        }
    }
}
