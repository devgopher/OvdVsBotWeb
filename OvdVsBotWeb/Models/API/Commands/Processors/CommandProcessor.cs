using OvdVsBotWeb.DataAccess;
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
        protected readonly IReadWriter<string> _chatStorage;

        protected CommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IReadWriter<string> chatStorage)
        {
            _messageTextManager = messageTextManager;
            _botClient = botClient;
            _chatStorage = chatStorage;
        }

        public async Task Process(long chatId, params string[] args) => await InnerProcess(chatId, args);

        protected abstract Task InnerProcess(long chatId, params string[] args);
    }
}
