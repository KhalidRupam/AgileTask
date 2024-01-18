using AgileTask.Models;
using AgileTask.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace AgileTask.Controllers
{

    public class ProductController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IProductService _productService;

        public ProductController(IToastNotification toastNotification, IProductService productService)
        {
            _toastNotification = toastNotification;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            string jwtToken = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(jwtToken))
            {
                return RedirectToAction("Index", "Home");
            }
            _toastNotification.AddSuccessToastMessage("SuccessFully logged in");
            var res= await _productService.getAllProduct();
            ProductViewModel productViewModel = new();
            productViewModel.products = res;
            return View(productViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(Product productView)
        {
            var res = await _productService.insertProduct(productView);
            ProductViewModel productViewModel = new();
            productViewModel.products = res;
            return View(productViewModel);
        }
    }
}
