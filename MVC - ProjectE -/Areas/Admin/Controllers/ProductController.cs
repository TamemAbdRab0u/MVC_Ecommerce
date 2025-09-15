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
        private readonly IWebHostEnvironment hostEnvironment;
        public ProductController(IUnitOfWork unitofwork, IWebHostEnvironment hostEnvironment)
        {
            this.unitofwork = unitofwork;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = unitofwork.Product.GetAll(includeProperties: "Category").ToList();  
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            Product product = unitofwork.Product.Get(x => x.Id == Id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Upsert(int? Id)
        {
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = unitofwork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            if (Id == null || Id == 0)  // Create
            {
                return View(productVM);
            }
            else                        // Update
            {
                productVM.Product = unitofwork.Product.Get(x => x.Id == Id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = hostEnvironment.WebRootPath;
                if(file != null)
                {
                    string FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string ProductPath = Path.Combine(wwwRootPath, @"images\products");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // Delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(ProductPath, FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\products\" + FileName;
                }

                if(productVM.Product.Id == 0)
                {
                    unitofwork.Product.Add(productVM.Product);
                }
                else
                {
                    unitofwork.Product.Update(productVM.Product);
                }
                unitofwork.Save();
                TempData["success"] = productVM.Product.Id == 0?"Product Added Successfully" : "Product Updated Successfully";
                return RedirectToAction("Index");
            }

            productVM.CategoryList = unitofwork.Category.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(productVM);

        }

        #region ApiCalls
        public IActionResult GetAll()
        {
            List<Product> products = unitofwork.Product.GetAll(includeProperties: "Category").ToList();
            return Json (new { data = products });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var product = unitofwork.Product.Get(x => x.Id == Id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            var oldImagePath = Path.Combine(hostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            unitofwork.Product.Remove(product);
            unitofwork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
