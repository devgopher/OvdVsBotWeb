using Hangfire;

namespace OvdVsBotWeb.Jobs
{
    public class JobManager : IJobManager
    {
        public void AddJob<T>(IJob<T> job, T message)
        {
            RecurringJob.AddOrUpdate(
                $"sendmsgjob_{job.Id}",
                () => job.DoIt(message),
                Cron.Daily);
        }

        public void RemoveJob<T>(IJob<T> job)
        {
            RecurringJob.RemoveIfExists($"sendmsgjob_{job.Id}");
        }
    }
}
