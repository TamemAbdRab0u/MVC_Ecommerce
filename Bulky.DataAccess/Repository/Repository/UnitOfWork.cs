using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {   
        private readonly AppDbContext context;
        public ICategoryRepo Category { get; private set; }
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            Category = new CategoryRepo(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
