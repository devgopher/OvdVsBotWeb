namespace OvdVsBotWeb.Settings
{
    public class BotSettings
    {
        public string DbConnection { get; set; }
        public int ChatPollingIntervalMs { get; set; }
        public string TelegramToken { get; set; }
    }
}
