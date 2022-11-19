using OvdVsBotWeb.Models.Data;

namespace OvdVsBotWeb.Services
{
    public interface IJobManagementService
    {
        Task StartAll();
        Task StartJob(Chat chat);
        Task StopJob(string chatId);
    }
}