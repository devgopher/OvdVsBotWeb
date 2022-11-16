using OvdVsBotWeb.DataAccess;

namespace OvdVsBotWeb.Models.Data
{
    public class Chat : IEntity<string>
    {
        public string ChatId { get; set; }
        public string Id { get => ChatId; set => ChatId = value; }
    }
}
