using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC___ProjectE__.Models;

namespace MVC___ProjectE__.ViewModel
{
	public class ProductVM
	{
		public Product Product { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> CategoryList { get; set; }
	}
}
