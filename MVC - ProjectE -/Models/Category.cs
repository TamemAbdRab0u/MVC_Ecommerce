using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC___ProjectE__.Models
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
		[Range(1, 100,ErrorMessage = "Between 1:100")]
		public int DisplayOrder { get; set; }

    }
}
