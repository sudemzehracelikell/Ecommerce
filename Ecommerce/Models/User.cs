namespace Ecommerce.Models
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? EMail { get; set; }
        public string? PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public int Code {get;set;}
        public Boolean State { get; set; }
        
        public string Password { get; set; }
        public ICollection<Order>?  Orders { get; set; }
        
    }
}