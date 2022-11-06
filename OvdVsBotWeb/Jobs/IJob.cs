namespace OvdVsBotWeb.Jobs
{
    public interface IJob<TObject>
    {
        public Guid Id { get; }
        Task DoIt(TObject input);
    }
}