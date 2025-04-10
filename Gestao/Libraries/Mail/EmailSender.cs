using Gestao.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Gestao.Libraries.Mail;

public class EmailSender(ILogger<EmailSender> logger, SmtpClient smtpClient, IConfiguration configuration) : IEmailSender<ApplicationUser>
{
    private readonly ILogger logger = logger;
    private readonly SmtpClient smtpClient = smtpClient;
    private readonly IConfiguration configuration = configuration;

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email,
        string confirmationLink) => SendEmailAsync(email, "Confirme seu e-mail",
        "<html lang=\"en\"><head></head><body>Por favor confirme sua conta clicando no link: " +
        $"<a href='{confirmationLink}'>clique aqui</a>.</body></html>");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email,
        string resetLink) => SendEmailAsync(email, "Redefina sua senha",
        "<html lang=\"en\"><head></head><body>Redefina sua senha clicando no link: " +
        $"<a href='{resetLink}'>clique aqui</a>.</body></html>");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email,
        string resetCode) => SendEmailAsync(email, "Redefina sua senha",
        "<html lang=\"en\"><head></head><body>Redefina sua senha " +
        $" usando o seguinte código:<br>{resetCode}</body></html>");

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        await Execute(subject, message, toEmail);
    }

    public async Task Execute(string subject, string message, string toEmail)
    {
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(configuration["EmailSender:User"]!);
        mailMessage.To.Add(new MailAddress(toEmail));
        mailMessage.Subject = subject;
        mailMessage.Body = message;
        mailMessage.IsBodyHtml = true;

        await smtpClient.SendMailAsync(mailMessage);        

        logger.LogInformation("Email to {EmailAddress} sent!", toEmail);
    }
}
