using MailKit.Net.Smtp;

using Microsoft.Extensions.Options;

using MimeKit;

using System.Collections.Generic;

using Example.Application.Interfaces;

namespace Example.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings settings;

        public EmailSender(IOptions<EmailSettings> options)
        {
            settings = options.Value;
        }

        public async void Send(string email, string subject, string message, List<string>? attachments = null)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(settings.Name, settings.Address));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = message
            };

            if (attachments != null)
                foreach (var item in attachments)
                    builder.Attachments.Add(item);

            emailMessage.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(settings.SmtpServer, settings.Port, settings.UseSSL);
            await smtp.AuthenticateAsync(settings.Login, settings.Password);
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);
        }
    }

    public class EmailSettings
    {
        public string Address { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SmtpServer { get; set; } = null!;
        public int Port { get; set; }
        public bool UseSSL { get; set; }
    }
}
