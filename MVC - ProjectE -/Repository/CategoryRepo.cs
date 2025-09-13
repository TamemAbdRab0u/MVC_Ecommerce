//using Bulky.DataAccess.Data;
//using Bulky.Models;
//using System.Linq.Expressions;

//namespace MVC___ProjectE__.Repository
//{
//    public class CategoryRepo : Repo<Category> , ICategoryRepo
//    {
//        AppDbContext context;
//        public CategoryRepo(AppDbContext context) : base(context)
//        {
//            this.context = context;
//        }

//        public void Save()
//        {
//            context.SaveChanges();
//        }

//        public void Update(Category obj)
//        {
//            context.Categories.Update(obj);
//        }
//    }
//}
