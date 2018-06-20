using OrderApp.Service.Commands;

namespace OrderApp.Service
{
    public interface ICommandService
    {   
        Command GetCommand(string commandName, string[] parameters);
    }
}
