using OvdVsBotWeb.DataAccess;
using OvdVsBotWeb.Models.API.Commands.Processors;
using OvdVsBotWeb.ResourceManagement;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace OvdVsBotWeb.Handlers
{
    public class BotUpdateHandler : IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly CommandProcessorFactory _cpFactory;
        private readonly MessageTextManager _messageTextManager;
        private readonly ILogger _logger;
        private const string simpleCommandPattern = @"\/([a-zA-Z0-9]*)$";
        private const string argsCommandPattern = @"\/([a-zA-Z0-9]*) (.*)";

        public BotUpdateHandler(ITelegramBotClient botClient,
            MessageTextManager messageTextManager,
            CommandProcessorFactory cpFactory,
            ILogger<BotUpdateHandler> logger)
        {
            _botClient = botClient;
            _messageTextManager = messageTextManager;
            _cpFactory = cpFactory;
            _logger = logger;
        }

        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient,
            Exception exception,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            try
            {
                var text = update.Message.Text;
                var command = "";
                var args = new List<string>(5);
                var result = "";

                var lang = SupportedLangs.EN; // NOTE: temporary!
                ProcessCommands(text, update.Message.Chat.Id, lang, ref command, ref args, ref result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(HandleUpdateAsync)} error: {ex.Message}!");
            }
        }

        private void ProcessCommands(string text,
            long chatId,
            SupportedLangs lang,
            ref string command,
            ref List<string> args,
            ref string result)
        {
            if (Regex.IsMatch(text, simpleCommandPattern))
            {
                command = Regex.Matches(text, simpleCommandPattern)
                    .FirstOrDefault()
                    .Groups[1].Value;
                _cpFactory.Get(command)
                    .Process(chatId);
            }
            else if (Regex.IsMatch(text, argsCommandPattern))
            {
                command = Regex.Matches(text, argsCommandPattern)
                    .FirstOrDefault()
                    .Groups[1].Value;

                var argsString = Regex.Matches(text, argsCommandPattern)
                                     .FirstOrDefault()
                                     .Groups[2].Value;


                if (!string.IsNullOrWhiteSpace(argsString))
                    args = argsString.Split(" ").ToList();

                _cpFactory.Get(command)
                    .Process(chatId, args?.ToArray());
            }
            else
            {
                result = _messageTextManager.GetText("ErroneousCommand", lang);
            }
        }
    }
}
