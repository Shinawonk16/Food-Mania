namespace Food_Mania.Models.Entities
{
    public class FoodOrder:BaseEntity
    {
        public string FoodId { get; set; }
        public string OrderId { get; set; }
        public int Quantity{get;set;}
        public Food Food { get; set; }
        public Order Order { get; set; }
    }
}