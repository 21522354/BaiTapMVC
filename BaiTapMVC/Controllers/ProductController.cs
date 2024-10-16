using BaiTapMVC.Models;
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
        public IActionResult ConfirmCreate()
        {
            var product = new Product();
            product.CatalogId = int.Parse(CatalogId);
            product.ProductCode = ProductCode;      
            product.ProductName = ProductName;
            if (Picture != null && Picture.Length > 0)
            {
                // Đường dẫn nơi lưu ảnh trong thư mục wwwroot/Hinh
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hinh", Picture.FileName);

                // Lưu file vào thư mục đích
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Picture.CopyTo(stream);
                }

                // Gán đường dẫn của file ảnh vào model để lưu vào database
                product.Picture = Picture.FileName;
            }
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
                if (Picture != null && Picture.Length > 0)
                {
                    // Đường dẫn nơi lưu ảnh trong thư mục wwwroot/Hinh
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hinh", Picture.FileName);

                    // Lưu file vào thư mục đích
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Picture.CopyTo(stream);
                    }

                    // Gán đường dẫn của file ảnh vào model để lưu vào database
                    product.Picture = Picture.FileName;
                }
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
