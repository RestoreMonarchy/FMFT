﻿using FMFT.Extensions.EmailClients.Models;

namespace FMFT.Web.Server.Models.Emails
{
    public class Email<TModel>
    {
        public string Subject { get; set; }
        public string EmailAddress { get; set; }
        public TModel Model { get; set; }

        public List<HtmlEmailMessageAttachment> Attachments { get; set; }
    }
}
