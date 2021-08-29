using DataAccesLayer;
using DataAccesLayer.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MailWebAPI.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IDbRepository _dbRepository;

        public MailService(IOptions<MailSettings> mailSettings, IDbRepository dbRepository)
        {
            _mailSettings = mailSettings.Value;
            _dbRepository = dbRepository;
        }

        public IQueryable<Mail> GetAllEmails()
        {
            return _dbRepository.GetAll();
        }

        public IQueryable<Mail> GetEmailBySubject(string subject)
        {
            return _dbRepository.Get(email => email.Subject == subject);
        }

        public async Task SendEmailAsync(MailDTORequest mailRequest)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = mailRequest.Subject
            };
            email.To.Add(MailboxAddress.Parse(mailRequest.Recipient));

            var builder = new BodyBuilder
            {
                HtmlBody = mailRequest.Body
            };

            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            email.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTlsWhenAvailable);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            Mail mail = new()
            {
                Recipient = mailRequest.Recipient,
                Subject = mailRequest.Subject,
                Body = mailRequest.Body,
                Date = System.DateTime.Now
            };
            await _dbRepository.Add(mail);
            await _dbRepository.SaveChangesAsync();
        }
    }
}
