namespace OvdVsBotWeb.Models.API.Commands.Validators
{
    public interface ICommandValidator<TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// Main validation procedure
        /// </summary>
        /// <returns></returns>
        public Task<bool> Validate(long chatId, params string[] args);

        /// <summary>
        /// A help for a concrete command
        /// </summary>
        /// <returns></returns>
        public string Help();
    }
}
