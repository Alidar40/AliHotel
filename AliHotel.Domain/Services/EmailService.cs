using System.Threading.Tasks;
using AliHotel.Domain.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace AliHotel.Domain.Services
{
    /// <summary>
    /// Service for working with emails. Is implementation of IEmailService.
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Sends email in behalf of administration
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            
            emailMessage.From.Add(new MailboxAddress("AliHotel administration", "administration@alihotel.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync("smtp.alihotel.com", 0, false);
                await client.AuthenticateAsync("administration@alihotel.com", "password");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
