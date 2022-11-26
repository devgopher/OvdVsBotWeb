using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.ResourceManagement;

namespace OvdVsBotWeb.Models.Data
{
    public class Chat : IEntity<string>
    {
        public string ChatId { get; set; }
        public SupportedLangs Lang { get; set; }
        public string Id { get => ChatId; set => ChatId = value; }
    }
}
