using System.ComponentModel.DataAnnotations;

namespace OvdVsBotWeb.Models.DB
{
    public class MessageViewModel
    {
        public Guid MessageId { get; set; }

        [MaxLength(100)]
        public string Subject { get; set; }

        [MaxLength(1000)]
        public string Body { get; set; }
    }
}
