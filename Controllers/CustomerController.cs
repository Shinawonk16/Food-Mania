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
    public class CustomerController : Controller
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IFoodService _foodService;
        public CustomerController(ICustomerService customerService, IFoodService foodService, ILogger<CustomerController> logger = null)
        {
            _customerService = customerService;
            _foodService = foodService;
            _logger = logger;
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
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CreateCustomerRequestModel model)
        {
            var register = _customerService.Create(model);
            if (register.Status == true)
            {
                TempData["message"] = register.Message;
                return RedirectToAction("Login", "User");
            }
            return StatusCode(406, "Registration Failed. Email may exist already");
        }
        [HttpGet]
        public IActionResult Update(string id)
        {
            var customer = _customerService.GetById(id);
            return View(customer.Data);
        }
        [HttpPost]
        public IActionResult Update(string id, UpdateCustomerRequestModel model)
        {
            var updateModel = _customerService.Update(id, model);
            if (updateModel.Status == true)
            {
                return RedirectToAction("#", "Customer");
            }
            return StatusCode(406, "Update Failed. ");
        }
        [HttpGet]
        public IActionResult FundWallet()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FundWallet(string id, decimal amount)
        {
            var fund = _customerService.FundWallet(id, amount);
            if (fund.Status == true)
            {
                return StatusCode(200, "Wallet Fund Successfully");
            }
            return StatusCode(406, "Unable To Fund Wallet Try Again Later ");
        }
        [HttpGet]
        public IActionResult CheckWallet(string id)
        {
            var wallet = _customerService.CheckWallet(id);
            if (wallet.Status == true)
            {
                ViewBag.Message = wallet.Message;
                return View();
            }
            return StatusCode(406, "Try Again Later ");

        }
        [HttpGet]
        public IActionResult Details(string id)
        {
            var customer = _customerService.GetById(id);
            return View(customer);
        }
            [HttpGet]
        public IActionResult GetAll()
        {
            var customer = _customerService.GetAll();
            return View(customer);
        }
        public IActionResult FoodStatus()
        {
            var food = _foodService.UpdateFoodStatus();
            return View(_foodService.GetAllFood().Data);
        }

    }
}