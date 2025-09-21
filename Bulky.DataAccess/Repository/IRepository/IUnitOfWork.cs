using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepo Category { get; }
        public IProductRepo Product { get; }
        public ICompanyRepo Company { get; }
        public IShoppingCartRepo ShoppingCart { get; }
        public IApplicationUserRepo ApplicationUser { get; }

        public void Save();
    }
}
