using BaiTapMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BaiTapMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly QuanLySanPhamContext _context;
        [BindProperty]
        public string CatalogId { get; set; }
        [BindProperty]
        public string ProductCode { get; set; }
        [BindProperty]
        public string ProductName { get; set; }
        [BindProperty]
        public IFormFile Picture { get; set; }
        [BindProperty]
        public string UnitPrice { get; set; }   
        public ProductController()
        {
            _context = new QuanLySanPhamContext();
        }
        //[Authorize]
        public IActionResult Index()
        {
            var listProduct = _context.Products.ToList();
            var listCatalog = _context.Catalogs.ToList();
            ViewBag.listCatalog = listCatalog;
            return View(listProduct);
        }
        public IActionResult Create()
        {
            IEnumerable<int> CatalogId = _context.Catalogs.ToList().Select(x => x.Id);
            ViewBag.CatalogId = new SelectList(CatalogId);
            return View();
        }
        public IActionResult Details(int Id)
        {
            var product = _context.Products.Find(Id);
            return View(product);
        }
        public IActionResult getListProduct(int iddm)
        {
            List<Product> products;

            if (iddm == -1)
            {
                products = _context.Products.Include(p => p.Catalog).ToList();
            }
            else
            {
                products = _context.Products.Where(p => p.CatalogId == iddm).Include(p => p.Catalog).ToList();
            }
            return PartialView("_ListProduct", products);   
        }
    }
}
