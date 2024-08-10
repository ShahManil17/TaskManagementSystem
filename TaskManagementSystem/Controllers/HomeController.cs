using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using NuGet.Versioning;
using System.Diagnostics;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.Repositories.Logins;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data.DataModels;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILogin _login;

        public static List<string>? Permissions = new List<string>();

        public HomeController(ILogger<HomeController> logger, ILogin login)
        {
            _logger = logger;
            _login = login;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            ReturnObject<List<string>>? permissionResponse = await _login.GetPermission(Convert.ToInt32(Request.Cookies["userId"]));
            if(permissionResponse != null && permissionResponse.IsSuccess)
            {
                Permissions = permissionResponse.Result;
                ViewBag.Permissions = permissionResponse.Result;
            }
            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            ReturnObject<Users>? response = await _login.LoginAsync(model);
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Message", response.ErrorMessage!=null ? response.ErrorMessage : "Oops, Something Went Wrong!");
                return View();
            }
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            Response.Cookies.Delete("userId");
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
