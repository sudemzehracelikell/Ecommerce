using System.ComponentModel.DataAnnotations;

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
}//Üye: id, adı, tipi(3 tane), e-posta, telefonNumarası, durum(akit, pasif)
    public enum UserType
    {
        Personal,
        Corporate,
        Distributor
    }
