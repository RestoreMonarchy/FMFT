﻿using FMFT.Web.Client.Brokers.APIs;
using FMFT.Web.Client.Models.Users;
using FMFT.Web.Client.Models.Users.Exceptions;
using RESTFulSense.WebAssembly.Exceptions;

namespace FMFT.Web.Client.Services.Foundations.Users
{
    public class UserService : IUserService
    {
        private readonly IAPIBroker apiBroker;

        public UserService(IAPIBroker apiBroker)
        {
            this.apiBroker = apiBroker;
        }

        public async ValueTask<List<User>> RetrieveAllUsersAsync()
        {
            return await apiBroker.GetAllUsersAsync();
        }

        public async ValueTask<User> RetrieveUserByIdAsync(int userId)
        {
            try
            {
                return await apiBroker.GetUserByIdAsync(userId);
            } catch (HttpResponseNotFoundException)
            {
                throw new UserNotFoundException();
            }            
        }
    }
}
