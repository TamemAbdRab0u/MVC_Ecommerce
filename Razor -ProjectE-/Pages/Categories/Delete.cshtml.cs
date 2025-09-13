using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor__ProjectE_.Data;
using Razor__ProjectE_.Models;

namespace Razor__ProjectE_.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
		private readonly AppDbContext context;
		public Category category { get; set; }
		public DeleteModel(AppDbContext context)
		{
			this.context = context;
		}
		public void OnGet(int Id)
        {
            category = context.Categories.FirstOrDefault(x => x.Id == Id);
        }

        public IActionResult OnPost()
        {
            context.Categories.Remove(category);
            context.SaveChanges();
			TempData["success"] = $"Category {category.Name} Deleted Successfully";
			return RedirectToPage("Index");
        }
    }
}
