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
                OrderHeader = new()
            };
            foreach(var cart in CartVM.Carts)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                CartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(CartVM);
        }

        public IActionResult Summary()
        {
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var UserId = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            CartVM = new()
            {
                Carts = unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == UserId, includeProperties: "Product"),
                OrderHeader = new()
            };

            CartVM.OrderHeader.ApplicationUser = unitOfWork.ApplicationUser.Get(x => x.Id == UserId);

            CartVM.OrderHeader.Name = CartVM.OrderHeader.ApplicationUser.Name;
            CartVM.OrderHeader.PhoneNumber = CartVM.OrderHeader.ApplicationUser.PhoneNumber;
            CartVM.OrderHeader.StreetAddress = CartVM.OrderHeader.ApplicationUser.StreetAddress;
            CartVM.OrderHeader.City = CartVM.OrderHeader.ApplicationUser.City;
            CartVM.OrderHeader.State = CartVM.OrderHeader.ApplicationUser.State;
            CartVM.OrderHeader.PostalCode = CartVM.OrderHeader.ApplicationUser.PostalCode;

            foreach (var cart in CartVM.Carts)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                CartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(CartVM);
        }
        public IActionResult Plus(int Id)
        {
            ShoppingCart cart = unitOfWork.ShoppingCart.Get(x => x.Id == Id);
            cart.Count += 1;
            unitOfWork.ShoppingCart.Update(cart);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Minus(int Id)
        {
            ShoppingCart cart = unitOfWork.ShoppingCart.Get(x => x.Id == Id);
            if(cart.Count <= 1)
            {
                unitOfWork.ShoppingCart.Remove(cart);
            }
            else
            {
                cart.Count -= 1;
                unitOfWork.ShoppingCart.Update(cart);
            }
                
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int Id)
        {
            ShoppingCart cart = unitOfWork.ShoppingCart.Get(x => x.Id == Id);
            unitOfWork.ShoppingCart.Remove(cart);
            unitOfWork.Save();
            return RedirectToAction("Index");
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
