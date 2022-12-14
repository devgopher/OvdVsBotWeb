using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Models.API.Commands.Validators;
using OvdVsBotWeb.Models.Commands;
using OvdVsBotWeb.Models.Data;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class RemoveScheduleCommandProcessor : CommandProcessor<RemoveSchedule>
    {
        public RemoveScheduleCommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IReadWriter<Chat, string> chatStorage,
            ILogger<RemoveScheduleCommandProcessor> logger,
            ICommandValidator<RemoveSchedule> validator) : base(messageTextManager, botClient, chatStorage, logger, validator)
        {
        }

        protected override Task InnerProcess(long chatId, params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
