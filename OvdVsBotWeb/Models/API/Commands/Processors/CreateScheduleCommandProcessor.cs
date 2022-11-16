using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Models.API.Commands;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class CreateScheduleCommandProcessor : CommandProcessor<CreateSchedule>
    {
        public CreateScheduleCommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IReadWriter<string> chatStorage) : base(messageTextManager, botClient, chatStorage)
        {
        }

        protected override Task InnerProcess(long chatId, params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
