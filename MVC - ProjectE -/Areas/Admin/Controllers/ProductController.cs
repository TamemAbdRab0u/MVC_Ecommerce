using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulky.Models.ViewModels;

namespace MVC___ProjectE__.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitofwork;
        public ProductController(IUnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = unitofwork.Product.GetAll().ToList();  
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            Product product = unitofwork.Product.Get(x => x.Id == Id);
            return View(product);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ProductVM productVM = new()
            {
                CategoryList = unitofwork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Product = new Product()
            };

            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM obj)
        {
            if (ModelState.IsValid)
            {
                unitofwork.Product.Add(obj.Product);
                unitofwork.Save();
                TempData["success"] = "Product Added Successfully";
                return RedirectToAction("Index");
            }

            obj.CategoryList = unitofwork.Category.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(obj);

        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Product product = unitofwork.Product.Get(x => x.Id == Id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                unitofwork.Product.Update(product);
                unitofwork.Save();
                TempData["success"] = "Product Updated Successfully";
                return RedirectToAction("Index");
            }
            return View("Edit", product);
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Product product = unitofwork.Product.Get(x => x.Id == Id);
            return View(product);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int Id)
        {
            Product product = unitofwork.Product.Get(x => x.Id == Id);
            if (product != null)
            {
                unitofwork.Product.Remove(product);
                unitofwork.Save();
                TempData["success"] = "Product Deleted Successfully";
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
