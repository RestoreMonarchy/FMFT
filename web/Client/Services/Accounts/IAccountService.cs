﻿using FMFT.Extensions.Blazor.Facebook.Models.Results;
using FMFT.Extensions.Blazor.Google.Models.Results;
using FMFT.Web.Client.Models.API.Accounts;

namespace FMFT.Web.Client.Services.Accounts
{
    public interface IAccountService
    {
        ValueTask HandleFacebookLoginAsync(FacebookLoginResult result);
        ValueTask HandleGoogleLoginAsync(GoogleLoginResult result);
        ValueTask InitializeAsync();
        ValueTask LoginAsync(AccountToken accountToken);
        ValueTask LogoutAsync();
        ValueTask<bool> UpdateUserAccountAsync();
    }
}