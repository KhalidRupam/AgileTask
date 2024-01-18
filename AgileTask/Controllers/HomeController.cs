using AgileTask.Models;
using AgileTask.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace AgileTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;
        public HomeController(IUserService userService, IToastNotification toastNotification)
        {
            _userService = userService;
            _toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            if (ModelState.IsValid) {
                var res = await _userService.authenticateUser(user);
                if (res == "User Not Found")
                {
                    ViewBag.error = "Invalid User Name or Password !!";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("token", res);
                    return RedirectToAction("Index", "Product");
                }
            }
            return View();
            
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}