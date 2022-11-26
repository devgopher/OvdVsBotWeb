using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Models.API.Commands.Validators;
using OvdVsBotWeb.Models.Commands;
using OvdVsBotWeb.Models.Data;
using OvdVsBotWeb.ResourceManagement;
using OvdVsBotWeb.Services;
using OvdVsBotWeb.Utils;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class LangCommandProcessor : CommandProcessor<Lang>
    {
        private readonly IJobManagementService _jobManagementService;
        private readonly ILogger<LangCommandProcessor> _logger;
        private readonly IJobManagementService jobManagement;

        public LangCommandProcessor(MessageTextManager messageTextManager,
            ILogger<LangCommandProcessor> logger,
            ITelegramBotClient botClient,
            IReadWriter<string> chatStorage,
            IJobManagementService jobManagement,
            ICommandValidator<Lang> validator) : base(messageTextManager, botClient, chatStorage, validator)
        {
            _logger = logger;
            this.jobManagement = jobManagement;
        }

        protected override async Task InnerProcess(long chatId, params string[] args)
        {
            _logger.LogInformation($"{this.GetType().Name}.{nameof(InnerProcess)}({String.Join(',', args)}) started...");
            
            if (!await _validator.Validate(chatId, args))
                await _botClient.SendTextMessageAsync(chatId, _validator.Help());

            var chat = (Chat)_chatStorage.Get(chatId.ToString());
            if (chat == default)
            {
                _logger.LogInformation($"Chat {chatId} wasn't found in a storage!");
                return;
            }

            chat.Lang = LangHelper.GetLang(args[0]);

            await _botClient.SendTextMessageAsync(chatId, _messageTextManager.GetText("LangMsg", chat.Lang));

            _chatStorage.Update(chat);

            // restarts messaging jobs
            await _jobManagementService.StopJob(chat.Id);
            await _jobManagementService.StartJob(chat);
        }
    }
}
