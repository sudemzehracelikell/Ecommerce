namespace Ecommerce.Models
{
    public class Order : BaseEntity
    {
        public int Code { get; set; }
        public DateTime OrderPlaced { get; set; }

        public string UserId { get; set; }
        public User? User { get; set; }
        
        public ICollection<OrderItem> OrderItems { get; set; }

    }    
}
