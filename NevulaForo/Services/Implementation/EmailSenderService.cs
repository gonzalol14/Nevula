using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using NevulaForo.Models.ViewModels;
using NevulaForo.Services.Contract;
using System.Net.Mail;

namespace NevulaForo.Services.Implementation
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _config;

        public EmailSenderService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(MailRequestVM request)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_config.GetSection("SmtpSettings:SenderName").Value, _config.GetSection("SmtpSettings:UserName").Value));
                message.To.Add(MailboxAddress.Parse(request.Email));
                message.Subject = request.Subject;
                message.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(
                        _config.GetSection("SmtpSettings:SmtpServer").Value, 
                        Convert.ToInt32(_config.GetSection("SmtpSettings:Port").Value),
                        SecureSocketOptions.StartTls
                    );
                    await client.AuthenticateAsync(_config.GetSection("SmtpSettings:UserName").Value, _config.GetSection("SmtpSettings:Password").Value);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
