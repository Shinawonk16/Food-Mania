using Food_Mania.Models.Enum;

namespace Food_Mania.Models.Entities
{
    public class Customer:BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        // public string NextOfKin { get; set; }
        public decimal Wallet { get; set; }
        
        // public Gender Gender { get; set; }
        public List<Order> Order { get; set; }
    }
}