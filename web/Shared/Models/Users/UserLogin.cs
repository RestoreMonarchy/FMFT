namespace FMFT.Web.Shared.Models.Users
{
    public class UserLogin
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
