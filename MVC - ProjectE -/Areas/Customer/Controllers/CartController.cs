using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using System.Security.Claims;

namespace MVC___ProjectE__.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public CartVM CartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var UserId = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            CartVM = new()
            {
                Carts = unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == UserId, includeProperties: "Product"),
                //OrderTotal = 0
            };
            foreach(var cart in CartVM.Carts)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                CartVM.OrderTotal += (cart.Price * cart.Count);
            }
            return View(CartVM);
        }

         private double GetPriceBasedOnQuantity(ShoppingCart cart)
         {
             if (cart.Count <= 50)
             {
                 return cart.Product.Price;
             }
             else
             {
                 if (cart.Count <= 100)
                 {
                     return cart.Product.Price50;
                 }
                 else
                 {
                     return cart.Product.Price100;
                 }
             }
         }
    }
}
