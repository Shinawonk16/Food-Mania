using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Food_Mania.Models.Dtos;
using Food_Mania.Models.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Food_Mania.Controllers
{
    // [Route("[controller]")]
    public class FoodController : Controller
    {
        private readonly ILogger<FoodController> _logger;
        private readonly IFoodService _foodService;


        public FoodController(ILogger<FoodController> logger, IFoodService foodService)
        {
            _logger = logger;
            _foodService = foodService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpGet]
        public IActionResult AddFood()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFood(CreateFoodRequestModel model)
        {
            var food = _foodService.Add(model);
            if (food.Status == true)
            {
                return StatusCode(200, "Food Added To Menu Successfully");
            }
            return StatusCode(406, "Registration Failed.");
        }

        public IActionResult GetAll()
        {

            _foodService.UpdateFoodStatus();
             var get=_foodService.GetAllFood();
            return View(get.Data);
        }
        [HttpGet]
        public IActionResult UpdateFood(string id)
        {
            var food = _foodService.GetById(id);
            return View(food.Data);
        }
        [HttpPost]
        public IActionResult UpdateFood(string id, UpdateFoodRequestModel model)
        {
            var food = _foodService.UpdateFood(id, model);
            if (food.Status == true)
            {
                return StatusCode(200, "Food Updated Successfully");
            }
            return RedirectToAction("AddFood","Food");
        }



    }
}