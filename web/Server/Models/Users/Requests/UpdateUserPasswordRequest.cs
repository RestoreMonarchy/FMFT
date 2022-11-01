namespace FMFT.Web.Server.Models.Users.Requests
{
    public class UpdateUserPasswordRequest
    {
        public int UserId { get; set; }
        public string PasswordText { get; set; }
    }
}
