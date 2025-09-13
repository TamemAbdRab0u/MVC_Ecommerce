using Microsoft.EntityFrameworkCore;
using MVC___ProjectE__.Data;
using MVC___ProjectE__.Repository.Repository;
using System.Linq.Expressions;

namespace MVC___ProjectE__.Repository
{
    public class Repo<T> : IRepo<T> where T : class
    {
        AppDbContext context;
        DbSet<T> dbSet;
        public Repo(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();

            // context.category = dbset
        }

        public void Add(T obj)
        {
            dbSet.Add(obj);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

		public T GetById(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
		{
			IQueryable<T> query;
			if (tracked)
			{
				query = dbSet;

			}
			else
			{
				query = dbSet.AsNoTracking();
			}

			query = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.FirstOrDefault();

		}

		public void Remove(T obj)
        {
            dbSet.Remove(obj);
        }
    }
}
