using OvdVsBotWeb.Models.API.Commands;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public abstract class CommandProcessor<TCommand> : ICommandProcessor
        where TCommand : class, ICommand
    {
        protected readonly MessageTextManager _messageTextManager;
        protected readonly ITelegramBotClient _botClient;

        protected CommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient)
        {
            _messageTextManager = messageTextManager;
            _botClient = botClient;
        }

        public async Task Process(long chatId, params string[] args) => await InnerProcess(chatId, args);

        protected abstract Task InnerProcess(long chatId, params string[] args);
    }
}
