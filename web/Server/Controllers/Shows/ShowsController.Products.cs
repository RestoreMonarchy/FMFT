using FMFT.Web.Server.Models.Accounts.Exceptions;
using FMFT.Web.Server.Models.ShowGalleries.Params;
using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.ShowProducts.Exceptions;
using FMFT.Web.Server.Models.ShowProducts.Params;
using Microsoft.AspNetCore.Mvc;

namespace FMFT.Web.Server.Controllers.Shows
{
    public partial class ShowsController
    {
        [HttpGet("{showId}/products")]
        public async ValueTask<IActionResult> GetShowProducts(int showId)
        {
            IEnumerable<ShowProduct> showProducts = await showCoordinationService.RetrieveShowProductsByShowIdAsync(showId);

            return Ok(showProducts);
        }

        [HttpPost("{showId}/products/add")]
        public async ValueTask<IActionResult> AddShowProduct(int showId, AddShowProductParams @params)
        {
            try
            {
                @params.ShowId = showId;
                ShowProduct showProduct = await showCoordinationService.AddShowProductAsync(@params);

                return Ok(showProduct);
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

        [HttpPost("{showId}/products/modify")]
        public async ValueTask<IActionResult> ModifyShowProduct(int showId, UpdateShowProductParams @params)
        {
            try
            {
                @params.ShowId = showId;
                ShowProduct showProduct = await showCoordinationService.ModifyShowProductAsync(@params);

                return Ok(showProduct);
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

        [HttpDelete("{showId}/products/{showProductId}/delete")]
        public async ValueTask<IActionResult> DeleteShowProduct(int showId, int showProductId)
        {
            try
            {
                await showCoordinationService.RemoveShowProductByIdAndShowIdAsync(showProductId, showId);

                return Ok();
            }
            catch (NotFoundShowProductException exception) 
            {
                return NotFound(exception);
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
