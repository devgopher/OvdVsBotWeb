using Hangfire;
using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Jobs;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;
using Chat = OvdVsBotWeb.Models.Data.Chat;
using Message = OvdVsBotWeb.Models.Data.Message;

namespace OvdVsBotWeb.Services
{
    public class JobManagementService : IJobManagementService
    {
        private readonly IReadWriter<string> _chatStorage;
        private readonly ITelegramBotClient _botClient;
        private readonly IServiceProvider _sp;
        private readonly IJobManager _jobManager;
        private readonly MessageTextManager _messageTextManager;

        public JobManagementService(ITelegramBotClient botClient,
            IReadWriter<string> chatStorage,
            IJobManager jobManager,
            IServiceProvider sp,
            MessageTextManager messageTextManager)
        {
            _chatStorage = chatStorage;
            _botClient = botClient;
            _jobManager = jobManager;
            _sp = sp;
            _messageTextManager = messageTextManager;
        }

        public async Task StartAll()
        {
            var chats = _chatStorage.GetAll();

            foreach (var chat in chats)
                await StartJob((Chat)chat);
        }

        public async Task StartJob(Chat chat)
        {
             var msg = new Message()
            {
                Body = "Stop inner dialog!! Mind yourself!!",
                Subject = "Reminder!",
                ChatId = chat.Id.ToString(),
                MessageId = Guid.NewGuid()
            };

            var job = _sp.GetRequiredService<RandomSendMessageJob>();

            _jobManager.AddJob(job, msg, Cron.Minutely());
        }
    }
}
