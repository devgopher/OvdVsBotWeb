using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Handlers;
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
        private readonly IJobManagementService _jobManagement;

        public LangCommandProcessor(MessageTextManager messageTextManager,
            ILogger<LangCommandProcessor> logger,
            ITelegramBotClient botClient,
            IReadWriter<Chat, string> chatStorage,
            IJobManagementService jobManagement,
            ICommandValidator<Lang> validator) : base(messageTextManager, botClient, chatStorage, logger, validator) 
            => _jobManagement = jobManagement;

        protected override async Task InnerProcess(long chatId, params string[] args)
        {
            try
            {
                _logger.LogInformation($"{this.GetType().Name}.{nameof(InnerProcess)}({String.Join(',', args)}) started...");

                if (!await _validator.Validate(chatId, args))
                    await _botClient.SendTextMessageAsync(chatId, _validator.Help());

                var chat = (Chat)_chatStorage.Get(chatId.ToString());
                if (chat == default)
                {
                    _logger.LogInformation($"Chat {chatId} wasn't found in a storage!");
                    return;
                } else if (!chat.IsActive)
                {
                    _logger.LogInformation($"Chat {chatId} is inactive!");
                    return;
                }

                chat.Lang = LangHelper.GetLang(args[0]);

                await _botClient.SendTextMessageAsync(chatId, _messageTextManager.GetText("LangMsg", chat.Lang));

                _chatStorage.Update(chat);

                // restarts messaging jobs
                await _jobManagement.StopJob(chat.Id);
                await _jobManagement.StartJob(chat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {nameof(LangCommandProcessor)}: {ex.Message}");
            }
        }
    }
}
