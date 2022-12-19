using FMFT.Emails.Server.Models;
using RestoreMonarchy.RazorViewEmailTemplates;
using RestoreMonarchy.RazorViewEmailTemplates.Models;
using RestoreMonarchy.RazorViewEmailTemplates.Services;

namespace FMFT.Emails.Server.Services
{
    public class ServerEmailGenerator
    {
        private readonly IEmailHtmlGenerator emailHtmlGenerator;

        public ServerEmailGenerator(IEmailHtmlGenerator emailHtmlGenerator)
        {
            this.emailHtmlGenerator = emailHtmlGenerator;
        }

        public async ValueTask<string> GenerateRegisterEmailHtmlAsync(RegisterEmailModel model)
        {
            EmailTemplateBuilder emailTemplateBuilder = new();
            emailTemplateBuilder.SetViewName("/Views/RegisterEmail.cshtml");
            emailTemplateBuilder.SetCulture("pl-PL");

            IEmailTemplate template = emailTemplateBuilder.Build();

            return await emailHtmlGenerator.GenerateEmailAsync(template, model);
        }

        public async ValueTask<string> GenerateResetPasswordEmailHtmlAsync(ResetPasswordEmailModel model)
        {
            EmailTemplateBuilder emailTemplateBuilder = new();
            emailTemplateBuilder.SetViewName("/Views/ResetPasswordEmail.cshtml");
            emailTemplateBuilder.SetCulture("pl-PL");

            IEmailTemplate template = emailTemplateBuilder.Build();

            return await emailHtmlGenerator.GenerateEmailAsync(template, model);
        }

        public async ValueTask<string> GenerateRegisterExternalEmailHtmlAsync(RegisterExternalEmailModel model)
        {
            EmailTemplateBuilder emailTemplateBuilder = new();
            emailTemplateBuilder.SetViewName("/Views/RegisterExternalEmail.cshtml");
            emailTemplateBuilder.SetCulture("pl-PL");

            IEmailTemplate template = emailTemplateBuilder.Build();

            return await emailHtmlGenerator.GenerateEmailAsync(template, model);
        }

        public async ValueTask<string> GenerateReservationSummaryEmailHtmlAsync(ReservationSummaryEmailModel model)
        {
            EmailTemplateBuilder emailTemplateBuilder = new();
            emailTemplateBuilder.SetViewName("/Views/ReservationSummaryEmail.cshtml");
            emailTemplateBuilder.SetCulture("pl-PL");

            IEmailTemplate template = emailTemplateBuilder.Build();

            return await emailHtmlGenerator.GenerateEmailAsync(template, model);
        }
    }
}
