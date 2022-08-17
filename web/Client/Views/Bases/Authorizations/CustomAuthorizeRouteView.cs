using FMFT.Web.Client.Services.Processings.Accounts;
using FMFT.Web.Shared.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace FMFT.Web.Client.Views.Bases.Authorizations
{
    public class CustomAuthorizeRouteView : RouteView
    {
        [Inject]
        public IAccountProcessingService AccountService { get; set; }

        [Parameter]
        public RenderFragment NotAuthorized { get; set; }

        protected override void Render(RenderTreeBuilder builder)
        {
            CustomAuthorizeAttribute authorizeAttribute = RouteData.PageType
                .GetCustomAttributes(typeof(CustomAuthorizeAttribute) ,true)
                .FirstOrDefault() 
                as CustomAuthorizeAttribute;

            if (authorizeAttribute != null && 
                (!AccountService.IsAuthenticated || authorizeAttribute.IsNotAuthorized(AccountService.Account.Role)))
            {
                RenderContentInDefaultLayout(builder, NotAuthorized);
                return;
            }

            base.Render(builder);
        }

        private void RenderContentInDefaultLayout(RenderTreeBuilder builder, RenderFragment content)
        {
            builder.OpenComponent<LayoutView>(0);
            builder.AddAttribute(1, nameof(LayoutView.Layout), DefaultLayout);
            builder.AddAttribute(2, nameof(LayoutView.ChildContent), content);
            builder.CloseComponent();
        }
    }
}
