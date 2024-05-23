using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Controller
{
    internal class EmailSender
    {
        string fromAddress = Environment.GetEnvironmentVariable("EmailUsername");
        string fromPassword = Environment.GetEnvironmentVariable("EmailSTMP");
        public void SendMail(string toAddress, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromAddress, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(fromAddress, toAddress, subject, body);

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not send the email. Error: " + ex.Message);
            }
        }
    }
}
