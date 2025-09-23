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
    public class OrderDetailRepo : Repository<OrderDetail>, IOrderDetailRepo
    {
        private readonly AppDbContext context;
        public OrderDetailRepo(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(OrderDetail obj)
        {
            context.OrderDetails.Update(obj);
        }
    }
}
