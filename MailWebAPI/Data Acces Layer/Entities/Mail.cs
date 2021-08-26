using System;

namespace DataAccesLayer.Entities
{
    public class Mail
    {
        public long Id { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }
}
