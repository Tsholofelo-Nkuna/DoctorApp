using DoctorManagement.Shared.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;



namespace DoctorManagement.Shared.Services
{
    public class MailSenderService 
    {
        private readonly EmailSettings _emailSettings;
        public MailSenderService(IOptions<EmailSettings> mailOptions)
        {
            _emailSettings = mailOptions.Value;
        }
        public async Task SendMailAsync(string toEmail, string subject, string htmlMessage)
        {
            using (var c = new  SmtpClient())
            {
                try
                {
                    var message = new MimeMessage();

                    message.From.Add(new MailboxAddress("EpicHealth", _emailSettings.SendFrom));
                    message.To.Add(new MailboxAddress(toEmail, toEmail));
                    message.Subject = subject;
                    message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

                    using (var client = new SmtpClient())
                    {
                        // client.IsSecure = true;
                        
                        client.Connect(_emailSettings.Smtp, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);

                        ////Note: only needed if the SMTP server requires authentication
                        client.Authenticate(_emailSettings.SendFrom, _emailSettings.Password);

                        var result = await client.SendAsync(message);
                        client.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {

                    
                }
            }
        }
        //public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
        //{
        //    throw new NotImplementedException();

        //}
        //public async Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
        //{
        //    using (var c = new SmtpClient())
        //    {
        //        var message = new MimeMessage();

        //        message.From.Add(new MailboxAddress("IzyBill", _emailSettings.SendFrom));
        //        message.To.Add(new MailboxAddress(user.UserName, email));
        //        message.Subject = "Reset Password";
        //        message.Body = new TextPart("html") { Text = @$"<p>Click <a href='{resetLink}'>here</a> to reset your password</p>" };

        //        using (var client = new SmtpClient())
        //        {
        //            // client.IsSecure = true;
        //            client.Connect(_emailSettings.Smtp, _emailSettings.Port, SecureSocketOptions.StartTls);

        //            ////Note: only needed if the SMTP server requires authentication
        //            client.Authenticate(_emailSettings.SendFrom, _emailSettings.Password);

        //            var result = client.Send(message);
        //            client.Disconnect(true);

        //        }
        //    }

        //}
    }
}
