using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MVC___ProjectE__.Data;
using MVC___ProjectE__.Models;
using MVC___ProjectE__.Repository;
using System.Diagnostics;

namespace MVC___ProjectE__.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepo CateRepo;
		private readonly AppDbContext context;

		public HomeController(ILogger<HomeController> logger, ICategoryRepo CateRepo, AppDbContext context)
        {
            _logger = logger;
            this.CateRepo = CateRepo;
			this.context = context;
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
