using System.Linq.Expressions;

namespace MVC___ProjectE__.Repository.Repository
{
    public interface IRepo<T> where T : class
    {
        public IEnumerable<T> GetAll();
		T GetById(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
		public void Add(T obj);
        public void Remove(T obj);
    }
}
