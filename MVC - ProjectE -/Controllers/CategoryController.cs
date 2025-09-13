using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;

namespace MVC___ProjectE__.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext context;
        private readonly ICategoryRepo CateRepo;
        public CategoryController(AppDbContext context, ICategoryRepo CateRepo)
        {
            this.context = context;
            this.CateRepo = CateRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Category> CategoryList = CateRepo.GetAll().ToList();
            return View(CategoryList);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            Category category = CateRepo.Get(x => x.Id == Id);
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
				CateRepo.Add(category);
                CateRepo.Save();
				TempData["success"] = "Category Added Successfully";
				return RedirectToAction("Index");
			}
            return View("Create",category);
		}


        [HttpGet]
        public IActionResult Edit(int Id)
        {
			Category category = CateRepo.Get(x => x.Id == Id);
			return View("Edit",category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                CateRepo.Update(category);
                CateRepo.Save();
				TempData["success"] = "Category Updated Successfully";
				return RedirectToAction("Index");
			}
            return View("Edit", category);
        }


        [HttpGet]
		public IActionResult Delete(int Id)
		{
			Category category = CateRepo.Get(x => x.Id == Id);
            return View("Delete", category);
		}
		[HttpPost]
		public IActionResult SaveDelete(int Id)
		{
            Category category = CateRepo.Get(x => x.Id == Id);
            CateRepo.Remove(category);
		    CateRepo.Save();
            TempData["success"] = "Category Deleted Successfully";         //  If We Need To Display Notifications
            return RedirectToAction("Index");
		}
	}
}
