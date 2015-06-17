using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Claudia.Site.Services
{
    public class Mailer : IDisposable, Claudia.Domain.EventManager.ISubscriber
    {
        public SmtpClient SmtpClient { get; set; }
        public MailMessage MailMessage { get; set; }

        public Mailer()
        {
            SmtpClient = new SmtpClient();
            MailMessage = new MailMessage();
        }
        public Mailer(string host, int port)
        {
            SmtpClient = new SmtpClient(host, port);
            MailMessage = new MailMessage();
        }
        public Mailer(string host, int port, MailMessage mail)
        {
            SmtpClient = new SmtpClient(host, port);
            MailMessage = mail;
        }

        public void SendMail()
        {
            SmtpClient.Send(MailMessage);
        }
        public async Task SendMailAsync()
        {
            await SmtpClient.SendMailAsync(MailMessage);
        }

        public void Dispose()
        {
            if (SmtpClient != null)
            {
                SmtpClient.Dispose();
            }
            if (MailMessage != null)
            {
                MailMessage.Dispose();
            }
        }

        public void SubscriptionUpdate(object message)
        {
            throw new NotImplementedException();
        }


    }
}