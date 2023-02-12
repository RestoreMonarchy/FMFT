using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FMFT.Web.Server.Controllers.Users
{
    public partial  class UsersController
    {
        [HttpGet("{userId}/orders")]
        public async ValueTask<IActionResult> GetUserOrders(int userId)
        {
            try
            {
                IEnumerable<Order> orders = await orderCoordinationService.RetrieveOrdersByUserIdAsync(userId);

                return Ok(orders);
            }
            catch (NotAuthenticatedAccountException exception)
            {
                return Unauthorized(exception);
            }
            catch (NotAuthorizedAccountException exception)
            {
                return Forbidden(exception);
            }
        }
    }
}
