using Hangfire;

namespace OvdVsBotWeb.Jobs
{
    public class JobManager : IJobManager
    {
        private readonly static Random _rand = new(DateTime.Now.Millisecond);

        public void AddJob<T>(IJob<T> job, T message, string cron)
        {
            RecurringJob.AddOrUpdate(
                $"sendmsgjob_{job.Id}",
                () => job.DoIt(message),
                cron);
        }

        public void AddJob<T>(IJob<T> job, T message)
            => AddJob(job, message, Cron.Hourly());

        public void RemoveJob<T>(IJob<T> job) 
            => RecurringJob.RemoveIfExists($"sendmsgjob_{job.Id}");
    }
}
