using System.Net.Mail;
using System.Net;
using Consulting.Auth.Application.Abstractions;

namespace Consulting.Auth.Application;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string email, string subject, string body)
    {
        using var smtp = new SmtpClient("localhost");

        smtp.Port = 1025;
        smtp.Credentials = new NetworkCredential("", "");
        smtp.EnableSsl = false;

        var mail = new MailMessage("no-reply@localhost.com", email, subject, body)
        {
            IsBodyHtml = true
        };
        await smtp.SendMailAsync(mail);
    }
}
