﻿using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Models.Users.Requests
{
    public class UpdateUserCultureRequest
    {
        public int UserId { get; set; }
        public CultureId CultureId { get; set; }
    }
}
