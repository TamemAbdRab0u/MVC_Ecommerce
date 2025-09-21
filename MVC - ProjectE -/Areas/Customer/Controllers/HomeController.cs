using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
using System.Diagnostics;
using Bulky.DataAccess.Repository.Repository;
using System.Security.Claims;

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
            ShoppingCart cart = new()
            {
                Product = unitofwork.Product.Get(x => x.Id == id, includeProperties: "Category"),
                Count = 1,
                ProductId = id,
            };
            return View(cart);
        }

        [HttpPost]
        public IActionResult Details(ShoppingCart Cart)
        {
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var UserId = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            Cart.ApplicationUserId = UserId;
            Cart.Id = 0;

            var CartFromDb = unitofwork.ShoppingCart.Get(x => x.ApplicationUserId == UserId && x.ProductId == Cart.ProductId);
            if (CartFromDb != null)          // Cart Exist
            {
                CartFromDb.Count += Cart.Count;
                unitofwork.ShoppingCart.Update(CartFromDb);
            }
            else
            {
                unitofwork.ShoppingCart.Add(Cart);
            }
            TempData["success"] = "Cart Updated Successfully";
            unitofwork.Save();
            return RedirectToAction(nameof(Index));
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
