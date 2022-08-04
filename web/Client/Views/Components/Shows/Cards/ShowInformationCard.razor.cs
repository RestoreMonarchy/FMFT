using FMFT.Web.Server.Models.Auditoriums;
using FMFT.Web.Server.Models.Shows;
using Microsoft.AspNetCore.Components;

namespace FMFT.Web.Client.Views.Components.Shows.Cards
{
    public partial class ShowInformationCard
    {
        [Parameter]
        public Show Show { get; set; }
        [Parameter]
        public Auditorium Auditorium { get; set; }
    }
}
