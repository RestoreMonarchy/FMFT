using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Users.Params
{
    public class UpdateUserRoleParams
    {
        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
