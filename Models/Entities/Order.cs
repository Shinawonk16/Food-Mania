namespace Food_Mania.Models.Entities
{
    public class Order: BaseEntity
    {
        public string CustomerId { get; set; }
        public bool IsDelivered { get; set; }
        public Customer Customer { get; set; }
        public Address Address { get; set; }
        public ICollection<FoodOrder> FoodOrders { get; set; } = new HashSet<FoodOrder>();
    }
}