namespace FMFT.Web.Server.Models.Users.Requests
{
    public class ChangeUserPasswordRequest
    {
        public int UserId { get; set; }
        public string CurrentPasswordText { get; set; }
        public string PasswordText { get; set; }
    }
}
