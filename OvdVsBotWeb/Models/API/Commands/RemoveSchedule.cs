using OvdVsBotWeb.Models.API.Commands;

namespace OvdVsBotWeb.Models.Commands;

public class RemoveSchedule : ICommand
{
    public Guid Id { get; set; }
}
