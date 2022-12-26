namespace FMFT.Web.Server.Models.Emails.Params
{
    public class ConfirmAccountEmailParams
    {
        public string FirstName { get; set; }
        public int UserId { get; set; }
        public Guid ConfirmSecret { get; set; }
    }
}
