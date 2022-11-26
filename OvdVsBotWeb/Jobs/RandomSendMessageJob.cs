using OvdVsBotWeb.Models.Data;
using Telegram.Bot;

namespace OvdVsBotWeb.Jobs
{
    public class RandomSendMessageJob : SendMessageJob
    {
        private readonly Random _rand = new(DateTime.Now.Millisecond);
        private readonly uint _factor;

        public RandomSendMessageJob(ITelegramBotClient botClient, ILogger<SendMessageJob> logger, uint factor)
            : base(botClient, logger) 
            => _factor = factor;

        public override async Task DoIt(Message msg)
        {
            if (_rand.Next() % _factor == 0)
                await base.DoIt(msg);
        }
    }

}
