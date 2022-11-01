namespace FMFT.Web.Client.Models.API.ResetPasswordRequests.Requests
{
    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; }
        public Guid SecretKey { get; set; }
    }
}
