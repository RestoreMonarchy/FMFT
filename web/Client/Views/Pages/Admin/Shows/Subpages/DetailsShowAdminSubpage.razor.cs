using FMFT.Web.Client.Models.API.Auditoriums;
using FMFT.Web.Client.Models.API.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Pages.Admin.Shows.Subpages
{
    public partial class DetailsShowAdminSubpage
    {
        [Parameter]
        public Show Show { get; set; }
        [Parameter]
        public List<Auditorium> Auditoriums { get; set; }

        public Auditorium Auditorium => Auditoriums.First(x => x.Id == Show.AuditoriumId);
    }
}
