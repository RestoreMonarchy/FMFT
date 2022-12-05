using FMFT.Web.Server.Brokers.QRCodes;
using FMFT.Web.Server.Models.QRCodes;

namespace FMFT.Web.Server.Services.Foundations.QRCodes
{
    public class QRCodeService : IQRCodeService
    {
        private readonly IQRCodeBroker qrCodeBroker;

        public QRCodeService(IQRCodeBroker qrCodeBroker)
        {
            this.qrCodeBroker = qrCodeBroker;
        }

        public async ValueTask<QRCodeImage> GenerateGuidQRCodeImageAsync(Guid guid)
        {
            return await qrCodeBroker.GenerateGuidQRCodeImageAsync(guid);
        }
    }
}
