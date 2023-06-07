using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Mania.Models.Dtos;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Enum;
using Food_Mania.Models.Repository.Interface;
using Food_Mania.Models.Service.Interface;
using Models.Dtos;
using Models.Entities;
using static UploadImages.Upload;

namespace Food_Mania.Models.Service.Implementation
{

    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IFoodImage _foodImages;

        public FoodService(IFoodRepository foodRepository, IFoodImage foodImages)
        {
            _foodRepository = foodRepository;
            _foodImages = foodImages;
        }

        public BaseResponse<FoodDto> Add(CreateFoodRequestModel model)
        {

            var images = _foodImages.UploadFile(model.Image);
            var foodReg = new Food
            {
                Type = model.Type,
                Price = model.Price,
                Image = images,
                Status = model.Status,
                Description = model.Description,
                AvailableTime = model.AvailableTime,
                NumberOfPlates = model.NumberOfPlates,
                // Categories = model.CategoryDto.
                Categories = new Category
                {
                    CategoryName = model.CategoryName,
                    Description = model.CategoryDescription,
                }
            };
            _foodRepository.Create(foodReg);
            _foodRepository.Save();
            return new BaseResponse<FoodDto>
            {
                Message = "Food Successfully Added",
                Status = true
            };
        }

        public BaseResponse<IEnumerable<FoodDto>> GetAllFood()
        {
            var foods = _foodRepository.GetAllFood();
            if (foods == null)
            {
                return null;
            }
            return new BaseResponse<IEnumerable<FoodDto>>()
            {
                Message = "Food list found successfully",
                Status = true,
                Data = foods.Select(x => new FoodDto
                {


                    Id = x.Id,
                    Type = x.Type,
                    Price = Math.Round(x.Price, 3),
                    Status = x.Status,
                    Image = x.Image,
                    Description = x.Description,
                    AvailableTime = x.AvailableTime,
                    NumberOfPlates = x.NumberOfPlates,
                    CategoryDto = new CategoryDto
                    {
                        CategoryName = x.Categories.CategoryName,
                        CategoryDescription = x.Categories.Description

                    }

                }).ToList()
            };



        }

        public BaseResponse<IEnumerable<FoodDto>> GetByCategory(string id)
        {
            var categories = _foodRepository.GetFoodByCategory(id);
            if (categories == null)
            {
                return null;
            }
            return new BaseResponse<IEnumerable<FoodDto>>()
            {
                Message = "Food list found successfully",
                Status = true,
                Data = categories.Select(x => new FoodDto
                {


                    Id = x.Id,
                    Type = x.Type,
                    Price = Math.Round(x.Price, 3),
                    Status = x.Status,
                    Image = x.Image,
                    Description = x.Description,
                    AvailableTime = x.AvailableTime,
                    NumberOfPlates = x.NumberOfPlates

                }).ToList()
            };

        }

        public BaseResponse<FoodDto> GetById(string id)
        {
            var food = _foodRepository.Get(i => i.Id == id);
            if (food == null)
            {
                return new BaseResponse<FoodDto>
                {
                    Message = "Food Not Found",
                    Status = false
                };
            }

            return new BaseResponse<FoodDto>
            {
                Message = "Food Found",
                Status = true,
                Data = new FoodDto
                {
                    Id = food.Id,
                    Type = food.Type,
                    Price = Math.Round(food.Price, 3),
                    Status = food.Status,
                    Image = food.Image,
                    Description = food.Description,
                    AvailableTime = food.AvailableTime,
                    NumberOfPlates = food.NumberOfPlates,
                    CategoryDto = new CategoryDto
                    {
                        CategoryName = food.Categories.CategoryName,
                        CategoryDescription = food.Categories.Description

                    }
                }
            };
        }

        public BaseResponse<FoodDto> UpdateFood(string id, UpdateFoodRequestModel model)
        {
            var food = _foodRepository.Get(id);
            if (food == null)
            {
                return new BaseResponse<FoodDto>
                {
                    Message = "Food not found",
                    Status = false
                };
            }


            var image = _foodImages.UploadFile(model.Image);
            food.Type = model.Type;
            food.Price = model.Price;
            food.Image = image;
            food.Status = model.Status;
            food.UpdatedAt = DateTime.Now;
            food.AvailableTime = model.AvailableTime;
            food.NumberOfPlates = model.NumberOfPlates;
            _foodRepository.Update(food);
            return new BaseResponse<FoodDto>
            {
                Message = "Food Updated Successfully",
                Status = true
            };
        }


        public bool UpdateFoodStatus()
        {
            var foods = _foodRepository.GetAllFood();
            foreach (var food in foods)
            {
                if (food.AvailableTime <= DateTime.Now && food.Status != FoodStatus.NotAvailable)
                {
                    food.Status = FoodStatus.Available;
                    var response = _foodRepository.Update(food);
                    return true;

                }
            }
            return false;
        }

    }
}