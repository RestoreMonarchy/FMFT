namespace FMFT.Web.Server.Models.Emails.Params
{
    public class RegisterEmailParams
    {
        public int UserId { get; set; }
        public Guid ConfirmSecret { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
    }
}
