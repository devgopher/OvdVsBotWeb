using Hangfire;
using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Jobs;
using OvdVsBotWeb.Models.Data;
using OvdVsBotWeb.ResourceManagement;
using OvdVsBotWeb.Services;
using Telegram.Bot;


namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class StartCommandProcessor : CommandProcessor<Start>
    {
        private readonly IJobManager _jobManager;
        private readonly IServiceProvider _sp;
        private readonly IJobManagementService _jobManagementService;



        public StartCommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IJobManager jobManager,
            IReadWriter<string> chatStorage,
            IServiceProvider sp,
            IJobManagementService jobManagementService) : base(messageTextManager, botClient, chatStorage)
        {
            _jobManager = jobManager;
            _sp = sp;
            _jobManagementService = jobManagementService;
        }

        protected override async Task InnerProcess(long chatId, params string[] args)
        {
            if (_chatStorage.Get(chatId.ToString()) != default)
                return;

            var chat = new Chat()
            {
                ChatId = chatId.ToString()
            };

            await _botClient.SendTextMessageAsync(chat.Id, _messageTextManager.GetText("HelloMsg", SupportedLangs.EN));

            _chatStorage.Add(chat);

            await _jobManagementService.StartJob(chat);
        }
    }
}
