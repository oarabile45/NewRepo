using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Cool_Co_Fridge_Management.EmailService
{
   
        public class EmailSender
        {
            private readonly string _smtpServer;
            private readonly int _smtpPort;
            private readonly string _fromEmail;
            private readonly string _fromPassword;

            public EmailSender(string smtpServer, int smtpPort, string fromEmail, string fromPassword)
            {
                _smtpServer = smtpServer;
                _smtpPort = smtpPort;
                _fromEmail = fromEmail;
                _fromPassword = fromPassword;
            }

            public async Task SendEmailAsync(string toEmail, string subject, string body, Attachment attachment = null)
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(toEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                if (attachment != null)
                {
                    mailMessage.Attachments.Add(attachment);
                }

                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_fromEmail, _fromPassword);
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }
}
