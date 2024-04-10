using Data.Common;
using Interfaces.Common.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Service.Common {
    public class MailService(IOptions<MailSettings> mailSettings) : IMailService {

        private readonly MailSettings _mailSettings = mailSettings.Value;

        public async Task SendEmailAsync(string toEmail, string subject, string body) {

            var email = new MimeMessage{
                Sender = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail)
            };

            email.From.Add(email.Sender);
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var builder = new BodyBuilder {
                HtmlBody = body
            };

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.SmtpUsername, _mailSettings.SmtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}