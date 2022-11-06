using OvdVsBotWeb.Models.Commands;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class RemoveScheduleCommandProcessor : CommandProcessor<RemoveSchedule>
    {
        public RemoveScheduleCommandProcessor(MessageTextManager messageTextManager, ITelegramBotClient botClient)
            : base(messageTextManager, botClient)
        {
        }

        protected override Task InnerProcess(long chatId, params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
