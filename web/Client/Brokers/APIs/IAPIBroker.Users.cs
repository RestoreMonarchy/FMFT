﻿using FMFT.Web.Client.Models.API;
using FMFT.Web.Client.Models.API.ShowProducts;
using FMFT.Web.Client.Models.API.ShowProducts.Requests;
using FMFT.Web.Client.Models.API.Shows;
using FMFT.Web.Client.Models.API.Shows.Requests;
using FMFT.Web.Client.Models.API.Users;
using FMFT.Web.Client.Models.API.Users.Requests;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse<List<User>>> GetAllUsersAsync();
        ValueTask<APIResponse<User>> GetUserByIdAsync(int userId);
        ValueTask<APIResponse> UpdateUserCultureAsync(UpdateUserCultureRequest request);
        ValueTask<APIResponse> UpdateUserRoleAsync(UpdateUserRoleRequest request);
        ValueTask<APIResponse> ConfirmUserEmailAsync(ConfirmUserEmailRequest request);
        ValueTask<APIResponse<List<UserLogin>>> GetUserLoginsByUserIdAsync(int userId);
        ValueTask<APIResponse> SendConfirmUserEmailAsync(int userId);
    }
}
