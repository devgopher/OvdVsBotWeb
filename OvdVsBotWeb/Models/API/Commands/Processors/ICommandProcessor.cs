namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public interface ICommandProcessor
    {
        Task Process(long chatId, params string[] args);
    }
}
