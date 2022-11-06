using OvdVsBotWeb.Models.Data;
using Telegram.Bot;

namespace OvdVsBotWeb.Jobs
{
    public class SendMessageJob : IJob<Message>
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger _logger;

        public SendMessageJob(ITelegramBotClient botClient, ILogger<SendMessageJob> logger)
        {
            _botClient = botClient;
            _logger = logger;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public async Task DoIt(Message msg)
        {
            try
            {
                _logger.LogInformation($"Sending a message {msg.MessageId}...");
                await _botClient.SendTextMessageAsync(msg.ChatId, $"{msg.Subject} \n {msg.Body}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sending a message {msg.MessageId} FAIL!");
            }
        }
    }
}
