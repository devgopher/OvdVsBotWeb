using OvdVsBotWeb.Models.Commands;

namespace OvdVsBotWeb.Models.API.Commands.Validators
{
    public class LangValidator : ICommandValidator<Lang>
    {
        public string Help() => "Command: /Lang <RU, EN...>";

        public async Task<bool> Validate(long chatId, params string[] args) 
            => !(args == default || args.Length < 1);
    }
}
