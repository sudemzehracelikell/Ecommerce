namespace Ecommerce.Models
{
    public class ProductCategory : BaseEntity
    {
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}