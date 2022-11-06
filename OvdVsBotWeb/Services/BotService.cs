using Microsoft.Extensions.Options;
using OvdVsBotWeb.Settings;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace OvdVsBotWeb.Services
{
    public class BotService : IHostedService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IUpdateHandler _updateHandler;
        private readonly ILogger<BotService> _logger;
        public BotService(ITelegramBotClient botClient,
            IUpdateHandler updateHandler,
            ILogger<BotService> logger)
        {
            _updateHandler = updateHandler;
            _botClient = botClient;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting BotService...");
            _botClient.StartReceiving(_updateHandler);
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping BotService...");
            await _botClient.CloseAsync(cancellationToken);
        }
    }
}
