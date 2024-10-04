using BaiTapMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public string Picture { get; set; }
        [BindProperty]
        public string UnitPrice { get; set; }   
        public ProductController()
        {
            _context = new QuanLySanPhamContext();
        }
        public IActionResult Index()
        {
            var listProduct = _context.Products.ToList();
            return View(listProduct);
        }
        public IActionResult Create()
        {
            IEnumerable<int> CatalogId = _context.Catalogs.ToList().Select(x => x.Id);
            ViewBag.CatalogId = new SelectList(CatalogId);
            return View();
        }
        public IActionResult ConfirmCreate()
        {
            var product = new Product();
            product.CatalogId = int.Parse(CatalogId);
            product.ProductCode = ProductCode;      
            product.ProductName = ProductName;      
            product.Picture = Picture;
            product.UnitPrice = double.Parse(UnitPrice);
            _context.Products.Add(product); 
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int Id)
        {
            var product = _context.Products.Find(Id);
            IEnumerable<int> CatalogId = _context.Catalogs.ToList().Select(x => x.Id);
            ViewBag.CatalogId = new SelectList(CatalogId);  
            return View(product);
        }
        public IActionResult ConfirmEdit(int Id)
        {
            var product = _context.Products.Find(Id);
            if(product != null)
            {
                product.ProductName = ProductName;
                product.ProductCode = ProductCode;
                product.CatalogId = int.Parse(CatalogId);
                product.Picture = Picture;
                product.UnitPrice = double.Parse(UnitPrice);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");   
        }
        public IActionResult ConfirmDelete(int Id)
        {
            var product = _context.Products.Find(Id);
            if(product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            var product = _context.Products.Find(Id);
            return View(product);
        }
        public IActionResult Delete(int Id)
        {
            var product = _context.Products.Find(Id);
            return View(product);
        }
    }
}
