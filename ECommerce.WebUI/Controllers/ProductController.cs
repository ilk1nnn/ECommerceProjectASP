using App.Business.Abstract;
using App.Entities.Models;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(int page = 1, int category = 0)
        {
            int pageSize = 10;
            var products = _productService.GetAllByCategory(category);
            var model = new ProductListViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                CurrentCategory = category,
                PageCount = (int)Math.Ceiling(products.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };
            return View(model);
        }

        public bool isClicked { get; set; } = false;


        public IActionResult HighestToLowest(int page = 1,int category = 0)
        {
            int pageSize = 10;
            var products = _productService.GetAllByCategory(category);
            var model = new ProductListViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).OrderBy(p => p.UnitPrice).ToList(),
                CurrentCategory = category,
                PageCount = (int)Math.Ceiling(products.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };
            return View(model);
        }

        public IActionResult LowestToHighest(int page = 1, int category = 0)
        {
            int pageSize = 10;
            var products = _productService.GetAllByCategory(category);
            var model = new ProductListViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(p => p.UnitPrice).ToList(),

                CurrentCategory = category,
                PageCount = (int)Math.Ceiling(products.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };
            return View(model);
        }

        public IActionResult SortAToZ(int page = 1, int category = 0)
        {
            int pageSize = 10;
            var products = _productService.GetAllByCategory(category);
            var model = new ProductListViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).OrderBy(p => p.ProductName).ToList(),
                CurrentCategory = category,
                PageCount = (int)Math.Ceiling(products.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };
            return View(model);
        }
        public IActionResult SortZToA(int page = 1, int category = 0)
        {
            int pageSize = 10;
            var products = _productService.GetAllByCategory(category);
            var model = new ProductListViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(p => p.ProductName).ToList(),
                CurrentCategory = category,
                PageCount = (int)Math.Ceiling(products.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentPage = page
            };
            return View(model);
        }


        [HttpGet]
        public IActionResult Add()
        {
            var model = new ProductAddViewModel();
            model.Product = new Product();
            model.Categories = _categoryService.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(ProductAddViewModel model)
        {
            _productService.Add(model.Product);
            return RedirectToAction("index");
        }
    }
}
