using FMFT.Web.Server.Models.ShowProducts.Params;
using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.Shows;
using FMFT.Web.Server.Models.Shows.Params;
using FMFT.Web.Server.Services.Foundations.ShowProducts;
using FMFT.Web.Server.Services.Orchestrations.Shows;
using FMFT.Web.Server.Services.Orchestrations.UserAccounts;
using FMFT.Web.Shared.Enums;
using Hangfire.Storage;
using FMFT.Web.Server.Models.UserAccounts;

namespace FMFT.Web.Server.Services.Coordinations.Shows
{
    public class ShowCoordinationService : IShowCoordinationService
    {
        private readonly IShowOrchestrationService showService;
        private readonly IUserAccountOrchestrationService userAccountService;

        public ShowCoordinationService(IShowOrchestrationService showService, IUserAccountOrchestrationService userAccountService)
        {
            this.showService = showService;
            this.userAccountService = userAccountService;
        }

        public async ValueTask<Show> AddShowAsync(AddShowParams @params)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await showService.AddShowAsync(@params);
        }

        public async ValueTask<Show> ModifyShowAsync(UpdateShowParams @params)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await showService.ModifyShowAsync(@params);
        }

        public async ValueTask<Show> ModifyShowSellingDetailsAsync(UpdateShowSellingDetailsParams @params)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await showService.ModifyShowSellingDetailsAsync(@params);
        }

        public async ValueTask<Show> RetrieveShowByIdAsync(int showId)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await showService.RetrieveShowByIdAsync(showId);
        }

        public async ValueTask<Show> RetrievePublicShowByIdAsync(int showId)
        {
            return await showService.RetrievePublicShowByIdAsync(showId);
        }

        public async ValueTask<IEnumerable<Show>> RetrievePublicShowsAsync()
        {
            return await showService.RetrievePublicShowsAsync();
        }
        
        public async ValueTask<IEnumerable<Show>> RetrieveAllShowsAsync()
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await showService.RetrieveAllShowsAsync();
        }

        public async ValueTask<IEnumerable<ShowProduct>> RetrieveShowProductsByShowIdAsync(int showId)
        {
            return await showService.RetrieveShowProductsByShowIdAsync(showId);
        }

        public async ValueTask<ShowProduct> AddShowProductAsync(AddShowProductParams @params)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await showService.AddShowProductAsync(@params);
        }

        public async ValueTask<ShowProduct> ModifyShowProductAsync(UpdateShowProductParams @params)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            return await showService.ModifyShowProductAsync(@params);
        }

        public async ValueTask RemoveShowProductByIdAndShowIdAsync(int showProductId, int showId)
        {
            await userAccountService.AuthorizeUserAccountByRoleAsync(UserRole.Admin);

            await showService.RemoveShowProductByIdAndShowIdAsync(showProductId, showId);
        }
    }
}
