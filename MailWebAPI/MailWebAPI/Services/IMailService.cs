using DataAccesLayer.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MailWebAPI.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailDTORequest mailRequest);
        IQueryable<Mail> GetAllEmails();
        IQueryable<Mail> GetEmailBySubject(string subject);
    }
}
