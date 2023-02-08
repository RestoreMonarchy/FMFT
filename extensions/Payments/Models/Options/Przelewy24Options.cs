namespace FMFT.Extensions.Payments.Models.Options
{
    public class Przelewy24Options
    {
        public int UserName { get; set; }
        public string UserSecret { get; set; }
        public string CRC { get; set; }
        public int MerchantId { get; set; }
        public int PosId { get; set; }
        public bool UseSandbox { get; set; }
    }
}
