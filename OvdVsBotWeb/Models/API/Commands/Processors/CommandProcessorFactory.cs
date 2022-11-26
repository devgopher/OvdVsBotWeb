using OvdVsBotWeb.Models.Commands;

namespace OvdVsBotWeb.Models.API.Commands.Processors
{
    public class CommandProcessorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandProcessorFactory(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public ICommandProcessor Get(string command)
        {
            var canonized = "";

            if (string.IsNullOrEmpty(command))
                throw new ArgumentNullException(nameof(command), "Can't be null or empty!");

            if (command.Length == 1)
                canonized = command.ToUpperInvariant();
            else
                canonized = $"{command.Substring(0, 1).ToUpperInvariant()}{command[1..].ToLowerInvariant()}";

            switch (canonized)
            {
                case nameof(CreateSchedule):
                    return _serviceProvider.GetRequiredService<CommandProcessor<CreateSchedule>>();
                case nameof(RemoveSchedule):
                    return _serviceProvider.GetRequiredService<CommandProcessor<RemoveSchedule>>();
                case nameof(Start):
                    return _serviceProvider.GetRequiredService<CommandProcessor<Start>>();
                case nameof(Stop):
                    return _serviceProvider.GetRequiredService<CommandProcessor<Stop>>();
                case nameof(Lang):
                    return _serviceProvider.GetRequiredService<CommandProcessor<Lang>>();
                default:
                    return _serviceProvider.GetRequiredService<CommandProcessor<Unknown>>();
            }

        }
    }
}
