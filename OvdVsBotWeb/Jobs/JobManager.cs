using Hangfire;

namespace OvdVsBotWeb.Jobs
{
    public class JobManager : IJobManager
    {
        public void AddJob<T>(string id, IJob<T> job, T message, string cron)
        {
             RecurringJob.AddOrUpdate(
                id,
                () => job.DoIt(message),
                cron);
        }

        public void AddJob<T>(string id, IJob<T> job, T message)
            => AddJob(id, job, message, Cron.Hourly());

        public void RemoveJob(string id)
            => RecurringJob.RemoveIfExists(id);
    }
}
