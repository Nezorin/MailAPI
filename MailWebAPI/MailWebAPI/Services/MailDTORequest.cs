using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MailWebAPI.Services
{
    public class MailDTORequest
    {
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }

    }
}
