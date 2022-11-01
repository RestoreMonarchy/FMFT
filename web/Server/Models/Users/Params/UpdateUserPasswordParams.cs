namespace FMFT.Web.Server.Models.Users.Params
{
    public class UpdateUserPasswordParams
    {
        public int UserId { get; set; }
        public string PasswordHash { get; set; }
    }
}
