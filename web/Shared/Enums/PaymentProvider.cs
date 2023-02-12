using System.ComponentModel;

namespace FMFT.Web.Shared.Enums
{
    public enum PaymentProvider
    {
        [Description("None")]
        None = 0,
        [Description("Mock")]
        Mock = 1,
        [Description("Przelewy24")]
        Przelewy24 = 2
    }
}
