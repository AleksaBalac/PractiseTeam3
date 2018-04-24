using System;
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
            message.To.Add(new MailboxAddress("electro", "electrodance@hotmail.rs"));
            message.Subject = "Registration";
            message.Body = new TextPart("plain")
            {
                Text = messageBody
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("aleksadeveloper@gmail.com", "d3v3l0per!espnet");
                
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }

        }
    }
}
