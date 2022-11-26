using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Models.API.Commands.Validators;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public abstract class CommandProcessor<TCommand> : ICommandProcessor
        where TCommand : class, ICommand
    {
        protected readonly MessageTextManager _messageTextManager;
        protected readonly ITelegramBotClient _botClient;
        protected readonly IReadWriter<string> _chatStorage;
        protected readonly ICommandValidator<TCommand> _validator;

        protected CommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IReadWriter<string> chatStorage,
            ICommandValidator<TCommand> validator)
        {
            _messageTextManager = messageTextManager;
            _botClient = botClient;
            _chatStorage = chatStorage;
            _validator = validator;
        }

        public async Task Process(long chatId, params string[] args)
        {
            if (await _validator.Validate(chatId, args))
                await InnerProcess(chatId, args);
            else
                await _botClient.SendTextMessageAsync(chatId, _validator.Help());
        }
        protected abstract Task InnerProcess(long chatId, params string[] args);
    }
}
