using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Core
{
    public static class EmailService
    {
        public static async Task SendEmail(string emailTo, string subject, string messageBody)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("PractiseTeam3", "aleksadeveloper@gmail.com"));
            message.To.Add(new MailboxAddress("User", emailTo));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = messageBody
            };

            message.Body = bodyBuilder.ToMessageBody();
            
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("aleksadeveloper@gmail.com", "d3v3l0per!espnet"); //don't try to be smart, there is nothing on this email :)
                
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}
