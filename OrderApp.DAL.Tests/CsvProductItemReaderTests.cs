using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderApp.DAL.CsvDataReader;

namespace OrderApp.DAL.Tests
{
    [TestClass]
    public class CsvProductItemReaderTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            CsvProductItemReader reader = new CsvProductItemReader(@"C:\Users\Renan\Downloads\order-price\order-price\sample_catalog.csv");

            // act 
            var result = reader.Read();

            // assert
            Assert.AreEqual(5, result.Count);
        }
    }
}
