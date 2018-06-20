using System.Text;

namespace OrderApp.Service.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand()
        {

        }

        public override OperationReturn<string> Process()
        {
            StringBuilder helpText = new StringBuilder();

            helpText.Append("Commands list:");

            helpText.Append("\n\n");

            helpText.Append("- CalculateOrder <file.csv> <ProductId> <Quantity> <ProductId> <Quantity> ...");

            helpText.Append("\n\n");

            OperationReturn<string> response = new OperationReturn<string>();
            response.Data = helpText.ToString();
            response.Success = true;

            return response;
        }

        public override OperationReturn<string> ValidateParameters()
        {
            return new OperationReturn<string>()
            {
                Success = true
            };
        }
    }
}
