using Food_Mania.Models.Dtos;
using Models.Entities;

namespace Food_Mania.Models.Service.Interface
{
    public interface IFoodService
    {  
       BaseResponse<FoodDto> Add(CreateFoodRequestModel model);
        BaseResponse<IEnumerable<FoodDto>> GetAllFood();
        BaseResponse<FoodDto> GetById(string id);
        BaseResponse<IEnumerable<FoodDto>> GetByCategory(string id);
        BaseResponse< FoodDto> UpdateFood(string id,UpdateFoodRequestModel model);
        bool UpdateFoodStatus();
    }
}