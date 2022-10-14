namespace FMFT.Web.Server.Models.Accounts
{
    public class AccountToken
    {
        public AccountToken(string token)
        {
            Token = token;
        }

        public AccountToken() { }

        public string Token { get; set; }
    }
}
