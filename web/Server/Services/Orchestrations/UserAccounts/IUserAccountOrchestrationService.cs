﻿using FMFT.Web.Server.Models.Accounts;
using FMFT.Web.Server.Models.UserAccounts.Requests;
using FMFT.Web.Server.Models.Users;
using FMFT.Web.Server.Models.Users.Params;

namespace FMFT.Web.Server.Services.Orchestrations.UserAccounts
{
    public interface IUserAccountOrchestrationService
    {
        ValueTask ChallengeExternalLoginAsync(string provider, string returnUrl);
        ValueTask<Account> ConfirmExternalLoginAsync(ConfirmExternalLoginRequest request);
        ValueTask<Account> HandleExternalLoginCallbackAsync();
        ValueTask<Account> RegisterWithPasswordAsync(RegisterWithPasswordRequest request);
        ValueTask<Account> RetrieveAccountAsync();
        ValueTask<IEnumerable<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByIdAsync(int userId);
        ValueTask<Account> SignInWithPasswordAsync(SignInWithPasswordRequest request);
        ValueTask SignOutAsync();
        ValueTask UpdateUserCultureAsync(UpdateUserCultureParams @params);
        ValueTask UpdateUserRoleAsync(UpdateUserRoleParams @params);
    }
}
