using Microsoft.AspNetCore.Mvc;
using MVC___ProjectE__.Data;
using MVC___ProjectE__.Models;
using MVC___ProjectE__.Repository;

namespace MVC___ProjectE__.Controllers
{
    public class CategoryController : Controller
    {
        public ICategoryRepo CatRepo;
        public CategoryController(ICategoryRepo CatRepo)
        {
            this.CatRepo = CatRepo;
        }

        public IActionResult Index()
        {
            List<Category> CategoryList = CatRepo.GetAll().ToList();
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
				CatRepo.Add(category);
                CatRepo.Save();
				TempData["success"] = "Category Added Successfully";
				return RedirectToAction("Index");
			}
            return View("Create",category);
		}

        public IActionResult Edit(int Id)
        {
			Category cat =CatRepo.GetById(x => x.Id == Id);
			return View("Edit",cat);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                CatRepo.Update(category);
                CatRepo.Save();
				TempData["success"] = "Category Updated Successfully";
				return RedirectToAction("Index");
			}
            return View("Edit", category);
        }

		public IActionResult Delete(int Id)
		{
			Category category = CatRepo.GetById(x => x.Id == Id);
			return View("Delete", category);
		}

		[HttpPost]
		public IActionResult SaveDelete(int Id)
		{
				Category category = CatRepo.GetById(x => x.Id == Id);
				CatRepo.Remove(category);
				CatRepo.Save();
                TempData["success"] = "Category Deleted Successfully";         //  If We Need To Display Notifications
                return RedirectToAction("Index");
		}
	}
}
