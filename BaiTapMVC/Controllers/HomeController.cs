using BaiTapMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BaiTapMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanLySanPhamContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = new QuanLySanPhamContext();
        }

        public IActionResult Index()
        {
            var models = _context.Products.ToList();
            ViewData["ListProduct"] = models;
            return View();
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
