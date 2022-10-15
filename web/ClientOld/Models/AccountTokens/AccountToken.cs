namespace FMFT.Web.Client.Models.AccountTokens
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
