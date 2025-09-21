using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.ViewModels
{
    public class CartVM
    {
        public double OrderTotal { get; set; }
        public IEnumerable<ShoppingCart> Carts { get; set; }
    }
}
