using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Models.API.Commands.Validators;
using OvdVsBotWeb.Models.Commands;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class RemoveScheduleCommandProcessor : CommandProcessor<RemoveSchedule>
    {
        public RemoveScheduleCommandProcessor(MessageTextManager messageTextManager, 
            ITelegramBotClient botClient,
            IReadWriter<string> chatStorage,
            ICommandValidator<RemoveSchedule> validator) : base(messageTextManager, botClient, chatStorage, validator)
        {
        }

        protected override Task InnerProcess(long chatId, params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
