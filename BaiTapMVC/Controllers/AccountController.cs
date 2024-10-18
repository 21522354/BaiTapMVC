using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BaiTapMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult ConfirmLogin()
        {
            string userName = Request.Form["UserName"];
            string password = Request.Form["Password"];

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName)
            };

            // Tạo claims identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Đăng nhập bằng cookie
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Giữ trạng thái đăng nhập ngay cả khi đóng trình duyệt
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30) // Thời gian hết hạn cookie
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

            return RedirectToAction("Index", "Product");
        }
    }
}
