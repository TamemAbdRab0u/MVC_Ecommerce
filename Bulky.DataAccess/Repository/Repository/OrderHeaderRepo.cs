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
    public class OrderHeaderRepo : Repository<OrderHeader>, IOrderHeaderRepo
    {
        private readonly AppDbContext context;
        public OrderHeaderRepo(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(OrderHeader obj)
        {
            context.OrderHeaders.Update(obj);
        }
    }
}
