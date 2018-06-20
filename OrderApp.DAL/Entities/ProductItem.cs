namespace OrderApp.DAL.Entities
{
    public class ProductItem
    {
        public ProductItem(string productId, int quantityInStock, decimal price)
        {
            this.ProductId = productId;
            this.QuantityInStock = quantityInStock;
            this.Price = price;
        }

        public string ProductId { get; }

        public int QuantityInStock { get; set; }

        public decimal Price { get; set; }
    }
}
