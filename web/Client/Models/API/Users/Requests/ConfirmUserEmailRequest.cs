namespace FMFT.Web.Client.Models.API.Users.Requests
{
    public class ConfirmUserEmailRequest
    {
        public int UserId { get; set; }
        public Guid ConfirmSecret { get; set; }
    }
}
