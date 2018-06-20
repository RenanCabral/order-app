using OrderApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace OrderApp.DAL.CsvDataReader
{
    /// <summary>
    /// Handles the logic of reading products from CSV source file.
    /// </summary>
    public class CsvProductItemReader : ICsvProductItemReader
    {
        public CsvProductItemReader(string csvFile)
        {
            _csvFile = csvFile;
        }

        private string _csvFile = null;

        /// <summary>
        /// Reads the products from csv file.
        /// </summary>
        /// <returns>Mapped list of ProductItem objects.</returns>
        public List<ProductItem> Read()
        {
            if (File.Exists(_csvFile) == false)
            {
                return null;
            }   

            string[] lines = File.ReadAllLines(_csvFile);
            List<ProductItem> productItemList = new List<ProductItem>();

            foreach (string line in lines)
            {
                string[] productData = line.Split(',');

                productItemList.Add(new ProductItem(productId: productData[0],
                                                    quantityInStock: Convert.ToInt32(productData[1]),
                                                    price: Convert.ToDecimal(productData[2], new CultureInfo("en-US"))
                                                    ));
            }

            return productItemList;
        }
    }
}
