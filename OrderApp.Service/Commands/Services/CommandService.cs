using OrderApp.Service.Commands;

namespace OrderApp.Service
{
    public class CommandService : ICommandService
    {
        private const char argumentSeparator = ' ';

        #region Public Methods

        public Command GetCommand(string commandName, string[] parameters)
        {
            Command command = CommandFactory.CreateInstance($"{commandName}Command", parameters);

            if (command == null)
            {
                return null;
            }

            return command;
        }

        #endregion
    }
}
