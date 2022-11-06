namespace FMFT.Web.Client.Models.API.Users
{
    public class UserLogin
    {
        public int UserId { get; set; }
        public string ProviderName { get; set; }
        public string ProviderKey { get; set; }
        public string FriendlyName { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
