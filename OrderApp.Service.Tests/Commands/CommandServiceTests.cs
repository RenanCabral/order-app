using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderApp.Service.Commands;

namespace OrderApp.Service.Tests.Commands
{
    [TestClass]
    public class CommandServiceTests
    {
        [TestMethod]
        public void Test()
        {
            //arrange 
            CommandService commandService = new CommandService();

            string commandName = "CalculateOrder";
            string[] parameters = new string[] { @"C:\Users\Renan\Downloads\order-price\order-price\sample_catalog.csv", "P1", "2" };

            //act
            Command command = commandService.GetCommand(commandName, parameters);

            //assert
            Assert.IsNotNull(command.Process());
        }

        [TestMethod]
        public void When_Given_Product_Quantity_Is_Out_Of_Stock_Expect_Error_With_Code_1()
        {
            //arrange 
            CommandService commandService = new CommandService();

            string commandName = "CalculateOrder";
            string[] parameters = new string[] { @"C:\Users\Renan\Downloads\order-price\order-price\sample_catalog.csv", "P1", "6" };

            //act
            Command command = commandService.GetCommand(commandName, parameters);

            //assert
            Assert.IsNotNull(command.Process());
        }
    }
}
