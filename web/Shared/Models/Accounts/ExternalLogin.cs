﻿namespace FMFT.Web.Shared.Models.Accounts
{
    public class ExternalLogin
    {
        public string ProviderName { get; set; }
        public string ProviderKey { get; set; }
        public Account Account { get; set; }
    }
}
