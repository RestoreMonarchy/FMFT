using FMFT.Web.Server.Services.Orchestrations.UserReservations;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace FMFT.Web.Server.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : RESTFulController
    {
        private readonly IUserReservationOrchestrationService userReservationService;

        public UsersController(IUserReservationOrchestrationService userReservationService)
        {
            this.userReservationService = userReservationService;
        }
    }
}
