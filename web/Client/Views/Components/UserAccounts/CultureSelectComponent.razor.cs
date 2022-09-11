using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Services.Views.UserAccounts;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.UserAccounts
{
    public partial class CultureSelectComponent
    {
        [Inject]
        public IUserAccountViewService UserAccountViewService { get; set; }

        public CultureId CultureId { get; set; }

        protected override void OnInitialized()
        {
            Account account = UserAccountViewService.RetrieveAccount();
            CultureId = account.CultureId;
        }

        public IEnumerable<CultureId> AvailableCultures => Enum.GetValues<CultureId>();
        public IEnumerable<CultureId> OtherCultures => AvailableCultures.Except(new[] { CultureId });

        private async Task HandleChangeCulture(CultureId cultureId)
        {
            CultureId = cultureId;
            await UserAccountViewService.ChangeCultureAsync(cultureId);
        }
    }
}
