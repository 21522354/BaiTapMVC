using BaiTapMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiTapMVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly QuanLySanPhamContext _context;
        [BindProperty]
        public string CatalogName { get; set; }
        [BindProperty]
        public string CatalogCode { get; set; }
        public CatalogController()
        {
            _context = new QuanLySanPhamContext();
        }
        public IActionResult Index()
        {
            var models = _context.Catalogs.ToList();
            return View(models);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult ConfirmCreate()
        {
            var catalog = new Catalog();
            catalog.CatalogName = CatalogName;
            catalog.CatalogCode = CatalogCode;
            _context.Catalogs.Add(catalog);
            _context.SaveChanges();
            return RedirectToAction("Index");   
        }
        public IActionResult Edit(int Id)
        {
            var catalog = _context.Catalogs.Find(Id);
            return View(catalog);   
        }
        public IActionResult ConfirmEdit(int Id)
        {
            var catalog = _context.Catalogs.Find(Id);
            catalog.CatalogName = CatalogName;
            catalog.CatalogCode = CatalogCode;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            var catalog = _context.Catalogs.Find(Id);
            return View(catalog);
        }
        public IActionResult Delete(int Id)
        {
            var catalog = _context.Catalogs.Find(Id);
            return View(catalog);
        }
        public IActionResult ConfirmDelete(int Id)
        {
            var catalog = _context.Catalogs.Find(Id);
            _context.Catalogs.Remove(catalog);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
