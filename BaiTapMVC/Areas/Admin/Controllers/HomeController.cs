using BaiTapMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();      
        }
    }
}
