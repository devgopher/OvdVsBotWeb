using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Models.API.Commands;
using OvdVsBotWeb.Models.API.Commands.Validators;
using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class CreateScheduleCommandProcessor : CommandProcessor<CreateSchedule>
    {
        public CreateScheduleCommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient,
            IReadWriter<string> chatStorage,
            ILogger<CreateScheduleCommandProcessor> logger,
            ICommandValidator<CreateSchedule> validator) : base(messageTextManager, botClient, chatStorage, logger, validator)
        {
        }

        protected override Task InnerProcess(long chatId, params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
