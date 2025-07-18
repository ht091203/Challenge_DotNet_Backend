using DotNetTraining.Domains.Entities;

namespace DotNetTraining.Domains.Dtos
{
    public class UserRoleDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
