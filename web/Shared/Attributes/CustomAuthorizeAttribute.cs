using FMFT.Web.Shared.Models.Users;

namespace FMFT.Web.Shared.Attributes
{
    public class CustomAuthorizeAttribute : Attribute
    {
        public UserRole[] UserRoles { get; set; }

        public CustomAuthorizeAttribute(params UserRole[] userRoles)
        {
            UserRoles = userRoles;
        }

        public bool IsAuthorized(UserRole userRole)
        {
            if (UserRoles.Length == 0)
                return true;

            return UserRoles.Contains(userRole);
        }

        public bool IsNotAuthorized(UserRole userRole)
        {
            return !IsAuthorized(userRole);
        }
    }
}
