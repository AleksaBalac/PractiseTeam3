using System;
using System.Net;
using System.Net.Mail;

namespace Core
{
    public class EmailService
    {
        public static void SendEmail(object sender, EventArgs e)
        {
            try
            {
                NetworkCredential basicCredential =
                    new NetworkCredential("aleksabalac@hotmail.com", "5CF4iFGQ8RPqg");

                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add("electrodance@outlook.com");
                mailMessage.From = new MailAddress("info@aleksab.com");
                mailMessage.Priority = MailPriority.High;

                mailMessage.Subject = "ASP.NET e-mail test";
                mailMessage.Body = "Hello world!";

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp-pulse.com";
                smtpClient.EnableSsl = false;
                smtpClient.Port = 2525;
                smtpClient.Credentials = basicCredential;

                smtpClient.Send(mailMessage);
                //Response.Write("E-mail sent!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Response.Write("Could not send the e-mail - error: " + ex.Message);
            }
        }
    }
}
