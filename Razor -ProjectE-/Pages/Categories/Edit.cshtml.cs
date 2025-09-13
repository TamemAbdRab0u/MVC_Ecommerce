using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor__ProjectE_.Data;
using Razor__ProjectE_.Models;

namespace Razor__ProjectE_.Pages.Categories
{
	[BindProperties]
	public class EditModel : PageModel
	{
		private readonly AppDbContext context;
		public Category category { get; set; }
		public EditModel(AppDbContext context)
		{
			this.context = context;
		}


		public void OnGet(int Id)
		{
			category = context.Categories.FirstOrDefault(x => x.Id == Id);
		}

		public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				context.Categories.Update(category);
				context.SaveChanges();
				TempData["success"] = $"Category {category.Name} Updated Successfully";
				return RedirectToPage("Index");
			}
			return Page();
		}
	}
}

