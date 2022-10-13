namespace FMFT.Web.Server.Models.Accounts.Params
{
    public class CreateTokenParams
    {
        public Account Account { get; set; }
        public string AuthenticationMethod { get; set; }
    }
}
