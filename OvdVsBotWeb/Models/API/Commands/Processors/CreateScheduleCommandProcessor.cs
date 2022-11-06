using OvdVsBotWeb.Models.API.Commands;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class CreateScheduleCommandProcessor : CommandProcessor<CreateSchedule>
    {
        public CreateScheduleCommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient) : base(messageTextManager, botClient)
        {
        }

        protected override Task InnerProcess(long chatId, params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
