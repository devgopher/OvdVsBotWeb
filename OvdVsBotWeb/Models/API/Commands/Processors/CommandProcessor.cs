using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Handlers;
using OvdVsBotWeb.Models.API.Commands.Validators;
using OvdVsBotWeb.Models.Data;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public abstract class CommandProcessor<TCommand> : ICommandProcessor
        where TCommand : class, ICommand
    {
        protected readonly MessageTextManager _messageTextManager;
        protected readonly ITelegramBotClient _botClient;
        protected readonly IReadWriter<Chat, string> _chatStorage;
        protected readonly ILogger _logger;
        protected readonly ICommandValidator<TCommand> _validator;

        protected CommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IReadWriter<Chat, string> chatStorage,
            ILogger logger,
            ICommandValidator<TCommand> validator)
        {
            _messageTextManager = messageTextManager;
            _botClient = botClient;
            _chatStorage = chatStorage;
            _logger = logger;
            _validator = validator;
        }

        public async Task Process(long chatId, params string[] args)
        {
            try
            {
                if (await _validator.Validate(chatId, args))
                    await InnerProcess(chatId, args);
                else
                    await _botClient.SendTextMessageAsync(chatId, _validator.Help());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {this.GetType().Name}: {ex.Message}");
            }
        }
        protected abstract Task InnerProcess(long chatId, params string[] args);
    }
}
