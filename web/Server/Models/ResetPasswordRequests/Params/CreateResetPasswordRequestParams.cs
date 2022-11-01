namespace FMFT.Web.Server.Models.ResetPasswordRequests.Params
{
    public class CreateResetPasswordRequestParams
    {
        public string Email { get; set; }
        public DateTimeOffset ExpireDate { get; set; }
    }
}
