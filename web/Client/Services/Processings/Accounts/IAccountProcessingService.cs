﻿using FMFT.Web.Client.Models.Accounts;
using FMFT.Web.Client.Models.Accounts.Requests;
using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Processings.Accounts
{
    public interface IAccountProcessingService
    {
        ValueTask ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request);
        ValueTask LoginAsync(LogInWithPasswordRequest request);
        ValueTask RegisterAsync(RegisterWithPasswordRequest request);
        ValueTask<Account> RetrieveAccountAsync();
    }
}