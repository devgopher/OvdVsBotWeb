namespace OvdVsBotWeb.Jobs
{
    public interface IJobManager
    {
        void AddJob<T>(string id, IJob<T> job, T message, string cron);
        void AddJob<T>(string id, IJob<T> job, T message);
        void RemoveJob(string id);
    }
}