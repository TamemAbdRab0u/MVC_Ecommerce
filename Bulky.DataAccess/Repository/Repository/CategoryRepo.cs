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
    public class CategoryRepo : Repository<Category>, ICategoryRepo
    {
        private readonly AppDbContext context;
        public CategoryRepo(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Category obj)
        {
            context.Categories.Update(obj);
        }
        public void Save()
        {
            context.SaveChanges();
        } 
    }
}
