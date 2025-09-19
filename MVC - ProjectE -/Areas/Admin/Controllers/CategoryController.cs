using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Bulky.Utility;

namespace MVC___ProjectE__.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly AppDbContext context;
        private readonly IUnitOfWork unitofwork;
        public CategoryController(AppDbContext context, IUnitOfWork unitofwork)
        {
            this.context = context;
            this.unitofwork = unitofwork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Category> CategoryList = unitofwork.Category.GetAll().ToList();
            return View(CategoryList);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            Category category = unitofwork.Category.Get(x => x.Id == Id);
            return View(category);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
		public IActionResult Create(Category category)
		{
			if (ModelState.IsValid)
			{
				unitofwork.Category.Add(category);
                unitofwork.Save();
				TempData["success"] = "Category Added Successfully";
				return RedirectToAction("Index");
			}
            return View("Create",category);
		}


        [HttpGet]
        public IActionResult Edit(int Id)
        {
			Category category = unitofwork.Category.Get(x => x.Id == Id);
			return View("Edit",category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                unitofwork.Category.Update(category);
                unitofwork.Save();
				TempData["success"] = "Category Updated Successfully";
				return RedirectToAction("Index");
			}
            return View("Edit", category);
        }


        [HttpGet]
		public IActionResult Delete(int Id)
		{
			Category category = unitofwork.Category.Get(x => x.Id == Id);
            return View("Delete", category);
		}
		[HttpPost]
		public IActionResult SaveDelete(int Id)
		{
            Category category = unitofwork.Category.Get(x => x.Id == Id);
            unitofwork.Category.Remove(category);
		    unitofwork.Save();
            TempData["success"] = "Category Deleted Successfully";         //  If We Need To Display Notifications
            return RedirectToAction("Index");
		}
	}
}
