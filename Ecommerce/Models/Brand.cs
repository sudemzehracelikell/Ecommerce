namespace Ecommerce.Models
{
    public class Brand : BaseEntity
    {
        public string? Name { get; set; }
        public int Code {get;set;}
        public string? Description { get; set; }
        public Boolean State { get; set; }
        
        public ICollection<Product>? Products { get; set; }
    }
}