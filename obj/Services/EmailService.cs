using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using TrabalhoLab.Models;

namespace TrabalhoLab.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using var client = new SmtpClient(_emailSettings.MailServer, _emailSettings.MailPort)
            {
                Credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password),
                EnableSsl = true,
            };

            await client.SendMailAsync(new MailMessage
            {
                From = new MailAddress(_emailSettings.Sender, _emailSettings.SenderName),
                To = { new MailAddress(email) },
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            });
        }
    }
}
