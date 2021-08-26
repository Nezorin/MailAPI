using DataAccesLayer.Entities;
using MailWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }
        [HttpGet]
        [Route("GetAllEmails")]
        public ActionResult<IEnumerable<Mail>> GetAllEmails()
        {
            return Ok(_mailService.GetAllEmails());
        }
        [HttpGet]
        [Route("GetBySubject/{subject}")]
        public ActionResult<IEnumerable<Mail>> GetEmailBySubject([FromRoute] string subject)
        {
            var result = _mailService.GetEmailBySubject(subject);
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("SendEmail")]
        public async Task<ActionResult<IEnumerable<Mail>>> SendEmailAsync([FromBody] MailDTORequest mailRequest)
        {
            await _mailService.SendEmailAsync(mailRequest);
            return Created(nameof(mailRequest), mailRequest);
        }
    }
}
