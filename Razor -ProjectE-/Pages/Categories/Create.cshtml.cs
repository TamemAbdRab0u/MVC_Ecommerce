using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor__ProjectE_.Data;
using Razor__ProjectE_.Models;

namespace Razor__ProjectE_.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public AppDbContext context;
        public Category category{ get; set; }

        public CreateModel(AppDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            context.Categories.Add(category);
            context.SaveChanges();
            TempData["success"] = $"Category {category.Name} Added Successfully";
            return RedirectToPage("Index");
        }
    }
}
