using FMFT.Extensions.Blazor.Bases.Loadings;
using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows
{
    public partial class ShowAdminPage
    {
        [Parameter]
        public int ShowId { get; set; }

        public string ShowName => ShowResponse?.Object?.Name ?? ShowId.ToString();

        public LoadingView LoadingView { get; set; }        

        public APIResponse<Show> ShowResponse { get; set; }
        public APIResponse<List<Auditorium>> AuditoriumsResponse { get; set; }
        
        public Show Show { get; set; }
        public List<Auditorium> Auditoriums => AuditoriumsResponse.Object;        

        protected override async Task OnParametersSetAsync()
        {
            if (!UserAccountState.IsInRole(UserRole.Admin))
            {
                return;
            }

            ShowResponse = await APIBroker.GetShowByIdAsync(ShowId);
            AuditoriumsResponse = await APIBroker.GetAllAuditoriumsAsync();
            
            if (ShowResponse.IsSuccessful)
            {
                Show = ShowResponse.Object;
            }

            LoadingView.StopLoading();
        }
    }
}
