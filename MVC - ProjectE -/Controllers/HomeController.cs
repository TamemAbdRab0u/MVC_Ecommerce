using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Bulky.DataAccess.Data;
using Bulky.Models;
using MVC___ProjectE__.Repository;
using System.Diagnostics;

namespace MVC___ProjectE__.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly AppDbContext context;

		public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
			this.context = context;
		}
        public IActionResult Index()
        {
            return View();
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
