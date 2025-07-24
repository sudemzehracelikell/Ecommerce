namespace Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public int Code {get;set;}
        public string? Description { get; set; }
        public Boolean State{ get; set; }

        public ICollection<ProductCategory>? ProductCategory { get; set; }
        
    }
}