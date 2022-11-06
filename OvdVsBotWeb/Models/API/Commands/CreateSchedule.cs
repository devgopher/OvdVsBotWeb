using OvdVsBotWeb.Models.Data;

namespace OvdVsBotWeb.Models.API.Commands
{
    public class CreateSchedule : ICommand
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
