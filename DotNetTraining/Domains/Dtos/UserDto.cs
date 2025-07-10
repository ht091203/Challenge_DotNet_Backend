using System.ComponentModel.DataAnnotations;

namespace DotNetTraining.Domains.Dtos
{
    public class UserDto
    {
        public Guid? Id { get; set; }
<<<<<<< HEAD
        [Required(ErrorMessage = "Tên không được để trống")]
=======
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        public string Name { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải từ 8 ký tự trở lên")]
        public string Password { get; set; }
    }
}
