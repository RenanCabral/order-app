using OrderApp.DAL.CsvDataReader;
using OrderApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.DAL.Repositories
{
    public class ProductItemRepository : IProductItemRepository
    {
        #region Constructors

        public ProductItemRepository(ICsvProductItemReader csvReader)
        {
            _productItems = csvReader.Read();
        }

        #endregion

        private static List<ProductItem> _productItems;

        public ProductItem GetProductItemById(string productId)
        {
            return _productItems.FirstOrDefault(x => x.ProductId == productId);
        }
    }
}
