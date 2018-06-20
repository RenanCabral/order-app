using OrderApp.DAL.CsvDataReader;

namespace OrderApp.DAL.Repositories
{
    public class ProductItemRepositoryFactory
    {
        public static IProductItemRepository CreateInstance(string csvFile)
        {
            CsvProductItemReader csvReader = new CsvProductItemReader(csvFile);
            return new ProductItemRepository(csvReader);
        }
    }
}
