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
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        private readonly IStaffService _staffService;
        private readonly IFoodService _foodService;

        public StaffController(ILogger<StaffController> logger, IFoodService foodService = null, IStaffService serviceService = null)
        {
            _logger = logger;
            _foodService = foodService;
            _staffService = serviceService;
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
        public IActionResult Add(CreateStaffRequestModel model)
        {
            var add = _staffService.Create(model);
            if (add.Status == true)
            {
               TempData["message"] = add.Message;
                return RedirectToAction("Login", "User");
            }
            return StatusCode(406, "Registration Failed. Email may exist already");


        }
        [HttpGet]
        public IActionResult Update(string id)
        {
            var staff = _staffService.GetById(id);
            // var updateModel = new UpdateStaffRequestModel
            // {
            //     FirstName = staff.Data.User.FirstName,
            //     LastName = staff.Data.User.LastName,
            //     PhoneNumber = staff.Data.User.PhoneNumber,
            //     Role = staff.Data.Role,
            //     Email = staff.Data.User.Email,
            //     DateOfBirth = staff.Data.DathOfBirth,
            // };
            return View(staff.Data);
        }
        [HttpPost]
        public IActionResult Update(string id, UpdateStaffRequestModel model)
        {

            _staffService.Update(id, model);
            return RedirectToAction("");
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var staff = _staffService.GetById(id);
            return View(staff.Data);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult ActualDelete(string id)
        {
            var del = _staffService.Delete(id);
            return RedirectToAction("Add");
        }
        [HttpGet]
        public IActionResult Details(string id)
        {
            var staff = _staffService.GetById(id);
            return View(staff);
        }
        [ActionName("StaffProfile")]
        public IActionResult Profile(string id)
        {
            //  if(HttpContext.Session.GetString("Role") != "Admin")
            // {
            //     return RedirectToAction("AdminLogin");
            // }
            var info = _staffService.VeiwProfile(id);
            return View(info);
        }
        [HttpGet]
        public IActionResult GetAll()
        {

            return View(_staffService.GetAll());
        }


    }
}