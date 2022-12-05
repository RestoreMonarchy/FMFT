namespace FMFT.Web.Server.Models.QRCodes.Params
{
    public class GenerateTicketParams
    {
        public string ReservationId { get; set; }
        public string ShowName { get; set; }
        public string UserName { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public string Branding { get; set; }
        public DateTimeOffset Date { get; set; }
        public Guid SecretCode { get; set; }
    }
}
