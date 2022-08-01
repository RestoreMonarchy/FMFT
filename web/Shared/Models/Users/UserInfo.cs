﻿using FMFT.Web.Shared.Models.Shared.Enums;

namespace FMFT.Web.Shared.Models.Users
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
    }
}
