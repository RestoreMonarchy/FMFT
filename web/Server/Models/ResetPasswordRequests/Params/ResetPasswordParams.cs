namespace FMFT.Web.Server.Models.ResetPasswordRequests.Params
{
    public class ResetPasswordParams
    {
        public string NewPassword { get; set; }
        public Guid SecretKey { get; set; }
    }
}
