using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
using System.Diagnostics;
using Bulky.DataAccess.Repository.Repository;

namespace MVC___ProjectE__.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitofwork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitofwork)
        {
            _logger = logger;
			this.unitofwork = unitofwork;
		}

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> Products = unitofwork.Product.GetAll(includeProperties: "Category").ToList();
            return View(Products);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Product product = unitofwork.Product.Get(x => x.Id == id, includeProperties: "Category");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
