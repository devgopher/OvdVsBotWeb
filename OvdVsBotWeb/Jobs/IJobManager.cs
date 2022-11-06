namespace OvdVsBotWeb.Jobs
{
    public interface IJobManager
    {
        void AddJob<T>(IJob<T> job, T message);
        void RemoveJob<T>(IJob<T> job);
    }
}