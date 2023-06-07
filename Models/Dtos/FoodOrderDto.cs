using Food_Mania.Models.Dtos;

namespace Models.Dtos
{
    public class FoodOrderDto
    {
        public bool IsDelivered { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public CustomerDto CustomerDto { get; set; }
        public FoodDto FoodDto { get; set; }
        public OrderDto OrderDto { get; set; }



    }
}