using Hangfire;
using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Jobs;
using OvdVsBotWeb.Models.Data;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;


namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class StartCommandProcessor : CommandProcessor<Start>
    {
        private readonly IJobManager _jobManager;
        private readonly IServiceProvider _sp;

        public StartCommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IJobManager jobManager,
            IReadWriter<string> chatStorage,
            IServiceProvider sp) : base(messageTextManager, botClient, chatStorage)
        {
            _jobManager = jobManager;
            _sp = sp;
        }

        protected override async Task InnerProcess(long chatId, params string[] args)
        {
            _chatStorage.Add(new Chat()
            {
                ChatId = chatId.ToString()
            });

            await _botClient.SendTextMessageAsync(chatId, _messageTextManager.GetText("HelloMsg", SupportedLangs.EN));

            var msg = new Message()
            {
                Body = "Stop inner dialog!! Mind yourself!!",
                Subject = "Reminder!",
                ChatId = chatId.ToString(),
                MessageId = Guid.NewGuid()
            };

            var job = _sp.GetRequiredService<RandomSendMessageJob>();

            _jobManager.AddJob(job, msg, Cron.Minutely());
        }
    }
}
