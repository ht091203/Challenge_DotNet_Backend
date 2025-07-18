using DotNetTraining.Domains.Entities;
using System.ComponentModel.DataAnnotations;

namespace DotNetTraining.Domains.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải từ 8 ký tự trở lên")]
        public string Password { get; set; }
        public Guid? RoleId { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }
    }
    public class UserLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserRegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
