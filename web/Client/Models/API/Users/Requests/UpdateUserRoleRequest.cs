using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.API.Users.Requests
{
    public class UpdateUserRoleRequest
    {
        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
