namespace OrderApp.Service.Factories
{
    public interface ICommandServiceFactory
    {
        ICommandService CreateInstance();
    }
}
