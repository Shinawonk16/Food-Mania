using Food_Mania.Models.Enum;
using Models.Entities;

namespace Food_Mania.Models.Entities
{
    public class Food : BaseEntity
    {
        public Category Categories { get; set; }
        public string CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int NumberOfPlates { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public FoodStatus Status { get; set; }
        public DateTime AvailableTime { get; set; }
        public ICollection<FoodOrder> FoodOrders { get; set; } = new HashSet<FoodOrder>();
    }
}