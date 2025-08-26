using Application.IServices;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendWelcomeEmail(string toEmail, string messageSend)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Library", "haihtam@gm.com"));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "New Book Created";
            message.Body = new TextPart("plain") { Text = $"{messageSend}" };

            using var client = new SmtpClient();
            client.Connect(_config["Smtp:Host"], int.Parse(_config["Smtp:Port"]), false);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
