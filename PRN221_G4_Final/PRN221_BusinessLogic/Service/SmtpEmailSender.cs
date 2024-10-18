using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PRN221_BusinessLogic.Interface;

namespace PRN221_BusinessLogic.Service
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly PRN221_Models.Models.Sender _config;
        public SmtpEmailSender(PRN221_Models.Models.Sender config)
        {
            _config = config;
        }
        public void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_config.EmailFrom);
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient(_config.SmtpAddress, _config.PortNumber))
                    {
                        smtp.Credentials = new NetworkCredential(_config.EmailFrom, _config.Password);
                        smtp.EnableSsl = _config.EnableSSL;
                        smtp.Send(mail);
                        Console.WriteLine("Email đã được chuyển qua SMTP");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gửi qua SMTP thất bại" + ex.Message);
            }
        }
    }
}
