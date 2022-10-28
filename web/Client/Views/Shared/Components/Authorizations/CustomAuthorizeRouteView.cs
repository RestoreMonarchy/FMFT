using FMFT.Web.Shared.Attributes;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using FMFT.Web.Client.StateContainers.UserAccounts;

namespace FMFT.Web.Client.Views.Shared.Components.Authorizations
{
    public class CustomAuthorizeRouteView : RouteView
    {
        [Parameter]
        public RenderFragment NotAuthorized { get; set; }
        [Parameter]
        public RenderFragment NotAuthenticated { get; set; }

        [Inject]
        public IUserAccountStateContainer UserAccountState { get; set; }

        protected override void Render(RenderTreeBuilder builder)
        {
            CustomAuthorizeAttribute authorizeAttribute = RouteData.PageType
                .GetCustomAttributes(typeof(CustomAuthorizeAttribute), true)
                .FirstOrDefault()
                as CustomAuthorizeAttribute;

            if (authorizeAttribute == null)
            {
                base.Render(builder);
                return;
            }

            if (!UserAccountState.IsAuthenticated)
            {
                RenderContentInDefaultLayout(builder, NotAuthenticated);
                return;
            }

            if (authorizeAttribute.IsNotAuthorized(UserAccountState.UserAccount.Role))
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
