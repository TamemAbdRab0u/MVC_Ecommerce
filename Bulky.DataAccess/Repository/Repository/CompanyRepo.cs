using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository.Repository
{
    public class CompanyRepo : Repository<Company>, ICompanyRepo
    {
        private readonly AppDbContext context;
        public CompanyRepo(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Company obj)
        {
            context.Companies.Update(obj);
        }
    }
}
