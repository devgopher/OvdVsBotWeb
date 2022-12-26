using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Models.API.Commands.Validators;
using OvdVsBotWeb.Models.Data;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class UnknownCommandProcessor : CommandProcessor<Unknown>
    {
        public UnknownCommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IReadWriter<Chat, string> chatStorage,
            ILogger<UnknownCommandProcessor> logger,
            ICommandValidator<Unknown> validator) : base(messageTextManager, botClient, chatStorage, logger, validator)
        {
        }

        protected override Task InnerProcess(long chatId, params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
