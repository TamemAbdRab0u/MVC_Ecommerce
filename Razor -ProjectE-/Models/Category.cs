using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Razor__ProjectE_.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Required*")]
		[MaxLength(30)]
		[DisplayName("Category Name")]
		public string Name { get; set; }

		[DisplayName("Display Order")]
		[Range(1, 100, ErrorMessage = "Between 1:100")]
		public int DisplayOrder { get; set; }
	}
}
