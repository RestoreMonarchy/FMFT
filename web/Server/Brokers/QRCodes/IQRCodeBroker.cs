﻿using FMFT.Web.Server.Models.QRCodes;

namespace FMFT.Web.Server.Brokers.QRCodes
{
    public interface IQRCodeBroker
    {
        Task<QRCodeImage> GenerateReservationQRCodeImageAsync(string reservationId);
    }
}