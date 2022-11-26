using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Models.API.Commands.Validators;
using OvdVsBotWeb.Models.Data;
using OvdVsBotWeb.ResourceManagement;
using OvdVsBotWeb.Services;
using Telegram.Bot;


namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class StartCommandProcessor : CommandProcessor<Start>
    {
        private readonly IJobManagementService _jobManagementService;

        public StartCommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IReadWriter<string> chatStorage,
            IJobManagementService jobManagementService,
            ICommandValidator<Start> validator) : base(messageTextManager, botClient, chatStorage, validator)
        {
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
