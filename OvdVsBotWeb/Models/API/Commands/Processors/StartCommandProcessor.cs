using OvdVsBotWeb.ResourceManagement;
using Telegram.Bot;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class StartCommandProcessor : CommandProcessor<Start>
    {
        public StartCommandProcessor(MessageTextManager messageTextManager,
            ITelegramBotClient botClient) : base(messageTextManager, botClient)
        {
        }

        protected override async Task InnerProcess(long chatId, params string[] args)
            => await _botClient.SendTextMessageAsync(chatId, _messageTextManager.GetText("HelloMsg", SupportedLangs.EN));
    }
}
