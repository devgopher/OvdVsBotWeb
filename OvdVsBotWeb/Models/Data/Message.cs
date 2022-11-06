using System.ComponentModel.DataAnnotations;

namespace OvdVsBotWeb.Models.Data
{
    public class Message
    {
        public string ChatId { get; set; }
        public Guid MessageId { get; set; }

        [MaxLength(100)]
        public string Subject { get; set; }

        [MaxLength(1000)]
        public string Body { get; set; }
    }
}
