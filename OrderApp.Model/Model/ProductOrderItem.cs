namespace OrderApp.Domain.Model
{
    public class ProductOrderItem
    {
        public ProductOrderItem(string productId, string quantity)
        {
            this.ProductId = productId;

            int.TryParse(quantity, out int parsedQuantity);
            this.Quantity = parsedQuantity;
        }

        public string ProductId { get; }

        public int Quantity { get; set; }
    }
}
