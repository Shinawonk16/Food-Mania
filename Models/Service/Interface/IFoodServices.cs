using Food_Mania.Models.Dtos;

namespace Food_Mania.Models.Service.Interface
{
    public interface IFoodServices
    {  
       BaseResponse<FoodDto> Add(CreateFoodRequestModel model);
        BaseResponse<List<FoodDto>> GetAllFood();
        BaseResponse<FoodDto> GetById(string id);
        BaseResponse<FoodDto> UpdateFood(UpdateFoodRequestModel model, string id);
        bool UpdateFoodStatus();
    }
}