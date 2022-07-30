namespace FMFT.Web.Shared.Models.Accounts.Params
{
    public class SignInAccountParams
    {
        public Account Account { get; set; }
        public bool IsPersistent { get; set; }
        public string AuthenticationMethod { get; set; }
    }
}
