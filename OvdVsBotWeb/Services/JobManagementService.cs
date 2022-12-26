using Hangfire;
using Hangfire.Common;
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
        private readonly IReadWriter<Chat, string> _chatStorage;
        private readonly ITelegramBotClient _botClient;
        private readonly IServiceProvider _sp;
        private readonly IJobManager _jobManager;
        private readonly MessageTextManager _messageTextManager;

        public JobManagementService(ITelegramBotClient botClient,
            IReadWriter<Chat, string> chatStorage,
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
                Body = _messageTextManager.GetText("OvdVsMsg", chat.Lang),
                Subject = string.Empty,
                ChatId = chat.Id.ToString(),
                MessageId = Guid.NewGuid()
            };

            var job = _sp.GetRequiredService<RandomSendMessageJob>();

            _jobManager.AddJob(GetJobId(chat.ChatId), job, msg, Cron.Minutely());
        }

        public async Task StopJob(string chatId) => _jobManager.RemoveJob(GetJobId(chatId));

        private string GetJobId(string chatId) => $"sendmsgjob_{chatId}";
    }
}
