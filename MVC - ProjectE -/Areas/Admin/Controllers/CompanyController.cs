using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MVC___ProjectE__.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Company> comapnies = unitOfWork.Company.GetAll().ToList();
            return View(comapnies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();    
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            unitOfWork.Company.Add(company);
            TempData["success"] = "Company created successfully";                
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            Company company = unitOfWork.Company.Get(x => x.Id == Id);
            return View(company);
        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Company.Update(company);
                TempData["success"] = "Company updated successfully";
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        #region ApiCalls
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Company> companies = unitOfWork.Company.GetAll().ToList();
            return Json(new { data = companies });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            Company? company = unitOfWork.Company.Get(x => x.Id == Id);
            if(company == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            unitOfWork.Company.Remove(company);
            unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
