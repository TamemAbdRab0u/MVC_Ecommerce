using MVC___ProjectE__.Models;
using MVC___ProjectE__.Repository.Repository;

namespace MVC___ProjectE__.Repository
{
    public interface ICategoryRepo : IRepo<Category>
    {
        public void Update(Category obj);
        public void Save();
    }
}
