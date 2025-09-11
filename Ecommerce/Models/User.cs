using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class User : IdentityUser 
        //, IBaseEntity
    {
        public string? Name { get; set; }
        
        public Boolean State { get; set; }

        
        public UserType UserType { get; set; }/*
        public int Code {get;set;}
        public string EMail { get; set; }
        public string Password { get; set; }*/
        public ICollection<Order>?  Orders { get; set; }

       // public int Id { get; set; }
    }
}