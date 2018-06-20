namespace OrderApp.Service.Factories
{
    public static class CommandServiceFactory
    {
        public static ICommandService CreateInstance()
        {
            return new CommandService();
        }
    }
}
