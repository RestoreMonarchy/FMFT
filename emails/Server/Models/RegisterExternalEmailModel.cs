namespace FMFT.Emails.Server.Models
{
    public class RegisterExternalEmailModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string AuthenticationMethod { get; set; }
    }
}
