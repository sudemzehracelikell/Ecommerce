using Ecommerce.models;
using ECommerce.Models;

namespace ECommerce.models
{
    public class Product
    {
        public int Id { get; set; }
        public int Code {get; set;}
        public string Name { get; set; }
        public float KDV { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }
        public Boolean State { get; set; }
        public string? Description { get; set; }
        
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        
        public ICollection<ProductOrder>? ProductOrders { get; set; }
        
    }
}//Product: *Brand, *Category, *name, *code, *VAT rate, status (active, passive), description, *price, *stock