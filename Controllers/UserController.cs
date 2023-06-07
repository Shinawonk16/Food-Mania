using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Food_Mania.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Service.Interface;

namespace Food_Mania.Controllers
{
    // [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService = null)
        {
            _logger = logger;
            _userService = userService;
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
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserRequestModel model)
        {
            var user = _userService.Login(model);
            
            if(!user.Status)
            {
                return View();
            }

           var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Data.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Data.Id),

                new Claim(ClaimTypes.Name, user.Data.FirstName +" "+ user.Data.LastName),
                new Claim(ClaimTypes.HomePhone, user.Data.PhoneNumber)
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

            HttpContext.SignInAsync(claimsPrincipal);
            
            if (user.Data.Role.ToString() == "Manager")
            {
                return RedirectToAction("Manager");
            }
           else if (user.Data.Role.ToString() == "Customer")
            {
                return RedirectToAction("Customer");
            }
          else   if (user.Data.Role.ToString() == "DeliveryMan")
            {
                return RedirectToAction("DeliveryMan");
            }
           else if (user.Data.Role.ToString() == "Cook")
            {
                return RedirectToAction("Cook");
            }
            else 
            {
                return   StatusCode(406, "Update Failed. ");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult Manager()
        {
            return View();
        }
        public IActionResult DeliveryMan()
        {
            return View();
        }
        public IActionResult Customer()
        {
            return View();
        }
        public IActionResult Cook()
        {
            return View();
        }
       
    }
}