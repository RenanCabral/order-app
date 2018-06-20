using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderApp.DAL.CsvDataReader;
using OrderApp.DAL.Entities;
using OrderApp.DAL.Repositories;
using OrderApp.Service.Commands;
using OrderApp.Service.Taxations;
using System.Collections.Generic;
using System.Linq;

namespace OrderApp.Service.Tests.Commands
{
    [TestClass]
    public class CalculateOrderCommandTests
    {
        #region Fields

        private Mock<ITaxationService> taxationServiceMock = null;

        private Mock<ICsvProductItemReader> csvReaderMock = null;

        private Mock<IProductItemRepository> productItemRepositoryMock = null;

        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            taxationServiceMock = new Mock<ITaxationService>();
            csvReaderMock = new Mock<ICsvProductItemReader>();
            productItemRepositoryMock = new Mock<IProductItemRepository>();
        }

        [TestMethod]
        public void When_Given_Product_Quantity_Is_Not_Out_Of_Stock_Expect_Valid_Amount_With_VAT_Applied_AsResult()
        {
            //arrange
            string[] parameters = new string[] { "C:/Test/file.csv", "P1", "3" };

            taxationServiceMock.Setup(x => x.GetTaxations()).Returns(new List<Taxation>() { new ValueAddedTax() });
            productItemRepositoryMock.Setup(x => x.GetProductItemById("P1")).Returns(new ProductItem("P1", 5, 100.00M));

            Command calculateOrderCommand = new CalculateOrderCommand(parameters, taxationServiceMock.Object, productItemRepositoryMock.Object);

            //act 
            OperationReturn<string> response = calculateOrderCommand.Process();

            //assert
            Assert.IsTrue(response.Success);
            Assert.AreEqual("Total: 369,00", response.Data);
        }

        [TestMethod]
        public void When_Given_Product_Quantity_Is_Out_Of_Stock_Expect_Error_With_Code_1()
        {
            //arrange
            string[] parameters = new string[] { "C:/Test/file.csv", "P2", "3" };

            taxationServiceMock.Setup(x => x.GetTaxations()).Returns(new List<Taxation>() { new ValueAddedTax() });
            productItemRepositoryMock.Setup(x => x.GetProductItemById("P2")).Returns(new ProductItem("P2", 2, 100.30M));

            Command calculateOrderCommand = new CalculateOrderCommand(parameters, taxationServiceMock.Object, productItemRepositoryMock.Object);

            //act 
            OperationReturn<string> response = calculateOrderCommand.Process();

            //assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("01", response.MessageList.First().Code);
            Assert.AreEqual("Product 'P2' is out of stock. Ordered quantity: 3; Available in stock: 2", response.MessageList.First().Message);
        }
    }
}
