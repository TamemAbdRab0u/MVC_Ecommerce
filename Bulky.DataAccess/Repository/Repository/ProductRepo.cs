using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.Repository
{
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        private readonly AppDbContext context;
        public ProductRepo(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Product obj)
        {
            context.Products.Update(obj);
        }
    }
}
