﻿using FMFT.Emails.Server.Models;
using FMFT.Web.Server.Models.Emails.Params;

namespace FMFT.Web.Server.Services.Foundations.Emails
{
    public interface IEmailService
    {
        ValueTask SendRegisterEmailAsync(string emailAddress, RegisterEmailParams @params);
        ValueTask SendRegisterExternalEmailAsync(string emailAddress, RegisterExternalEmailParams @params);
        ValueTask SendReservationSummaryEmailAsync(string emailAddress, ReservationSummaryEmailParams @params);
        ValueTask SendResetPasswordEmailAsync(string emailAddress, ResetPasswordEmailParams resetPasswordEmailParams);
    }
}