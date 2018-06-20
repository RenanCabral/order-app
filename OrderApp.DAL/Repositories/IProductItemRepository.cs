using OrderApp.DAL.Entities;

namespace OrderApp.DAL.Repositories
{
    public interface IProductItemRepository
    {
        ProductItem GetProductItemById(string productItemId);
    }
}
