using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Services.Views.UserAccountCultures;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.UserAccountCultures
{
    public partial class CultureSelectComponent
    {
        [Inject]
        public IUserAccountCultureViewService UserAccountViewService { get; set; }

        public CultureId CultureId { get; set; }

        protected override async void OnInitialized()
        {
            CultureId = await UserAccountViewService.RetrieveCultureIdAsync();
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
