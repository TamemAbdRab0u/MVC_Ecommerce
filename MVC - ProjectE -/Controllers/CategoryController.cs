using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Data;
using Bulky.Models;

namespace MVC___ProjectE__.Controllers
{
    public class CategoryController : Controller
    {
        AppDbContext context;
        public CategoryController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            List<Category> CategoryList = context.Categories.ToList();
            return View(CategoryList);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Create(Category category)
		{
			if (ModelState.IsValid)
			{
				context.Categories.Add(category);
                context.SaveChanges();
				TempData["success"] = "Category Added Successfully";
				return RedirectToAction("Index");
			}
            return View("Create",category);
		}

        public IActionResult Edit(int Id)
        {
			Category cat =context.Categories.FirstOrDefault(x => x.Id == Id);
			return View("Edit",cat);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Update(category);
                context.SaveChanges();
				TempData["success"] = "Category Updated Successfully";
				return RedirectToAction("Index");
			}
            return View("Edit", category);
        }

		public IActionResult Delete(int Id)
		{
			Category category = context.Categories.FirstOrDefault(x => x.Id == Id);
			return View("Delete", category);
		}

		[HttpPost]
		public IActionResult SaveDelete(int Id)
		{
            Category category = context.Categories.FirstOrDefault(x => x.Id == Id);
                context.Categories.Remove(category);
				context.SaveChanges();
                TempData["success"] = "Category Deleted Successfully";         //  If We Need To Display Notifications
                return RedirectToAction("Index");
		}
	}
}
