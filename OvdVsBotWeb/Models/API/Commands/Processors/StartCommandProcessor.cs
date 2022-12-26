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
            IReadWriter<Chat, string> chatStorage,
            IJobManagementService jobManagementService,
            ILogger<StartCommandProcessor> logger,
            ICommandValidator<Start> validator) : base(messageTextManager, botClient, chatStorage, logger, validator)
        {
            _jobManagementService = jobManagementService;
        }

        protected override async Task InnerProcess(long chatId, params string[] args)
        {
            var chat = _chatStorage.Get(chatId.ToString()) as Chat;
            if (chat != default && chat.IsActive)
                return;
            else
            {
                if (chat == default)
                {
                    chat = new Chat()
                    {
                        ChatId = chatId.ToString(),
                        IsActive = true
                    };

                    _chatStorage.Add(chat);
                }
                else
                {

                    chat.IsActive = true;
                    _chatStorage.Update(chat);
                }
            }

            await _botClient.SendTextMessageAsync(chat.Id, _messageTextManager.GetText("HelloMsg", SupportedLangs.EN));


            await _jobManagementService.StartJob(chat);
        }
    }
}
