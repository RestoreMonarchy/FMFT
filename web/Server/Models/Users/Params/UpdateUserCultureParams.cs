using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Users.Params
{
    public class UpdateUserCultureParams
    {
        public int UserId { get; set; }
        public CultureId CultureId { get; set; }
    }
}
