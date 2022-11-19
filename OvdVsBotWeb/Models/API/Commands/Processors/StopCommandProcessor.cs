using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Models.Data;
using OvdVsBotWeb.ResourceManagement;
using OvdVsBotWeb.Services;
using Telegram.Bot;


namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class StopCommandProcessor : CommandProcessor<Stop>
    {
        private readonly IJobManagementService _jobManagementService;

        public StopCommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IReadWriter<string> chatStorage,
            IJobManagementService jobManagementService) : base(messageTextManager, botClient, chatStorage)
        {
            _jobManagementService = jobManagementService;
        }

        protected override async Task InnerProcess(long chatId, params string[] args)
        {
            if (_chatStorage.Get(chatId.ToString()) == default)
                return;

            await _botClient.SendTextMessageAsync(chatId, _messageTextManager.GetText("GoodByeMsg", SupportedLangs.EN));

            _chatStorage.Remove(chatId.ToString());

            await _jobManagementService.StopJob(chatId.ToString());
        }
    }
}
