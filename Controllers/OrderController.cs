using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Food_Mania.Models.Dtos;
using Food_Mania.Models.Enum;
using Food_Mania.Models.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Food_Mania.Controllers
{
    // [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IFoodService _foodService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService = null, ICustomerService customerService = null, IFoodService foodService = null)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
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
        public IActionResult OrderFood()
        {
            return View();
        }
        [HttpPost]
        public IActionResult OrderFood(string Id, CreateOrderRequestModel model)
        {
            var customerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var get = _foodService.GetById(Id);
            if (get == null || get.Data.Status == FoodStatus.NotAvailable || get.Data.Status == FoodStatus.Processing)
            {
                return StatusCode(406,"Food not available at the moment");
            }
            else
            {
                var calculate = _orderService.CalculatePrice(customerId, get.Data.Price,model.Quantity);
                if (calculate.Status == false)
                {
                    return Content(calculate.Message);
                }
                else
                {
                    var order = _orderService.CreateOrder(customerId, Id, model);
                    ViewBag.Message = order.Message + "," + calculate.Message;
                    return View();
                }
            }

        }
         public IActionResult GetAllOrder()
        {
           var get =  _orderService.ViewOrders();
            return View(get.Data);
        } 
        public IActionResult DeliveryStatus(string id)
        {
           var order =  _orderService.UpdateStatus(id);
           if(order.Status == true)
           {
            ViewBag.Message = order.Message;
             return View();
           }
           return RedirectToAction("GetAllOrder");
        }
        [ActionName("OrderProfile")]
         public IActionResult CustomerOrderProfile(string id)
        {
            var profile =  _orderService.GetOrderById(id);
            return View(profile.Data);
        }
    }
}