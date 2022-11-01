using FMFT.Web.Server.Models.Users;

namespace FMFT.Web.Server.Models.ResetPasswordRequests
{
    public class ResetPasswordRequest
    {
        public int Id { get; set; }
        public Guid SecretKey { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsExpired { get; set; }
        public DateTime ResetDate { get; set; }
        public bool IsReset { get; set; }
        public DateTime CreateDate { get; set; }

        public UserInfo User { get; set; }
    }
}
