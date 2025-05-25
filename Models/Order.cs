/*namespace WebApp1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public DateTime OrderPlaced { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
} */

namespace WebApp1.models
{
    public class Order
    {
        public int Id { get; set; } 
        public int Code { get; set; }
        public DateTime OrderPlaced { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }    
}
