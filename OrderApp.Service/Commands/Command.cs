namespace OrderApp.Service.Commands
{
    public abstract class Command
    {
        public string Name { get; set; }
        
        public string[] Parameters { get; set; }

        public abstract OperationReturn<string> ValidateParameters();

        public abstract OperationReturn<string> Process();
    }
}