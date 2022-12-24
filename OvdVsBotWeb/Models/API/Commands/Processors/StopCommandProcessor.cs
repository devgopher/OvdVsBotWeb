using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Models.API.Commands.Validators;
using OvdVsBotWeb.Models.Data;
using OvdVsBotWeb.ResourceManagement;
using OvdVsBotWeb.Services;
using OvdVsBotWeb.Utils;
using Telegram.Bot;


namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class StopCommandProcessor : CommandProcessor<Stop>
    {
        private readonly IJobManagementService _jobManagementService;

        public StopCommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IReadWriter<string> chatStorage,
            IJobManagementService jobManagementService,
            ILogger<StopCommandProcessor> logger,
            ICommandValidator<Stop> validator) : base(messageTextManager, botClient, chatStorage, logger, validator)
        {
            _jobManagementService = jobManagementService;
        }

        protected override async Task InnerProcess(long chatId, params string[] args)
        {
            var chat = (Chat)_chatStorage.Get(chatId.ToString());
            if (chat == default)
                return;

            await _botClient.SendTextMessageAsync(chatId, _messageTextManager.GetText("GoodByeMsg", chat.Lang));

            _chatStorage.Remove(chatId.ToString());

            await _jobManagementService.StopJob(chatId.ToString());
        }
    }
}
