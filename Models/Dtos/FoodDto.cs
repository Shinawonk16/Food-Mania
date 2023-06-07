using Food_Mania.Models.Enum;
using Models.Dtos;
using Models.Entities;

namespace Food_Mania.Models.Dtos
{
    public class FoodDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPlates { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public CategoryDto CategoryDto { get; set; }
        public FoodStatus Status { get; set; }
        public DateTime AvailableTime { get; set; }
    }
    public class CreateFoodRequestModel
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPlates { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
         public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public FoodStatus Status { get; set; }
        public DateTime AvailableTime { get; set; }
    }
    public class UpdateFoodRequestModel
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int NumberOfPlates { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public FoodStatus Status { get; set; }
        public DateTime AvailableTime { get; set; }
    }
}