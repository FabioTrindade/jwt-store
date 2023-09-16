using JwtStore.Core;
using JwtStore.Core.AccountContext.Entities;
using JwtStore.Core.AccountContext.UseCases.Create.Contract;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace JwtStore.Infra.Contexts.AccountContext.Usecases.Create;

public class Service : IService
{
    public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
    {
        var client = new SendGridClient(Configuration.SendGrid.ApiKey);
        var from = new EmailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
        const string subject = "Verifique sua conta";
        var to = new EmailAddress(user.Email, user.Name);
        var content = $"Código {user.Email.Verification.Code}";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
        await client.SendEmailAsync(msg, cancellationToken);
    }
}