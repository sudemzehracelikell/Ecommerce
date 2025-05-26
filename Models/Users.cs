using System.ComponentModel.DataAnnotations;
using ECommerce.Models;

namespace WebApp1.models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public int Code {get;set;}
        public Boolean State { get; set; }
        
        public ICollection<Order>  Orders { get; set; }
    }
}//Member: id, name, type (3), email, phone number, status (active, passive)
    
