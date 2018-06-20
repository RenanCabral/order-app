namespace OrderApp.Service.Commands
{
    public class CommandFactory
    {
        public static Command CreateInstance(string commandName, string[] parameters)
        {
            return ObjectCreator<Command>.CreateInstance(commandName, parameters);
        }
    }
}