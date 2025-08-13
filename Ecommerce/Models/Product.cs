using System.Text.Json.Serialization;

namespace Ecommerce.Models
{
    public class Product: BaseEntity
    {
        public int Code { get; set; }
        public string? Name { get; set; }
        public float KDV { get; set; }
        public float BasePrice { get; set; }
        public int Stock { get; set; }
        public Boolean State { get; set; }
        public string? Description { get; set; }

        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }

        [JsonIgnore] // ? 
        public ICollection<ProductCategory>? ProductCategory { get; set; }
        
        [JsonIgnore]
        public ICollection<ProductVariant>? ProductVariants { get; set; }
         
    }
}