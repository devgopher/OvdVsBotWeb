using Telegram.Bot;
using Telegram.Bot.Polling;

namespace OvdVsBotWeb.Services
{
    public class BotService : IHostedService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IUpdateHandler _updateHandler;
        private readonly ILogger<BotService> _logger;
        private readonly IJobManagementService _jobManagementService;

        public BotService(IServiceProvider sp)
        {
            _updateHandler = sp.GetRequiredService<IUpdateHandler>();
            _botClient = sp.GetRequiredService<ITelegramBotClient>();
            _logger = sp.GetRequiredService<ILogger<BotService>>();
            _jobManagementService = sp.GetRequiredService<IJobManagementService>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting BotService...");
            _botClient.StartReceiving(_updateHandler);
            await _jobManagementService.StartAll();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping BotService...");
            await _botClient.CloseAsync(cancellationToken);
        }
    }
}
