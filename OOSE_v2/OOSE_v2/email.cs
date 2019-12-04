using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace OOSE_v2
{
    static public class Email
    {
        static public void send_email(string reciver, string subject, string body)
        {
            MailAddress sender = new MailAddress("msaedu.temp@gmail.com");
            MailAddress R = new MailAddress(reciver);
            MailMessage mail = new MailMessage();
            mail.From = sender;
            mail.To.Add(R);
            mail.Body = body;
            mail.Subject = subject;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("msaedu.temp@gmail.com", "QWE123456789q");
            smtp.Host = "smtp.gmail.com";
            //smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
