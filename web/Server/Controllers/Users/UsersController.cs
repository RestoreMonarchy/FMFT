using FMFT.Web.Server.Services.Orchestrations.Reservations;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : RESTFulController
    {
        private readonly IReservationOrchestrationService reservatonService;

        public UsersController(IReservationOrchestrationService reservationService)
        {
            this.reservatonService = reservationService;
        }
    }
}
