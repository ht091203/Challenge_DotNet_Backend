using System.ComponentModel.DataAnnotations;

namespace DotNetTraining.Domains.Dtos
{
    public class CategoryDto
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
        public string Name { get; set; }
    }
}
