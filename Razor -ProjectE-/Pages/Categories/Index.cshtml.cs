using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor__ProjectE_.Data;
using Razor__ProjectE_.Models;

namespace Razor__ProjectE_.Pages.Categories
{
    public class IndexModel : PageModel
    {
        public AppDbContext context;
        public List<Category> CategoryList { get; set; }

		public IndexModel(AppDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            CategoryList = context.Categories.ToList();
        }
    }
}
