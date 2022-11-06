namespace OvdVsBotWeb.Models.Data
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public TimeOnly FromTime { get; set; }
        public TimeOnly ToTime { get; set; }
    }
}
