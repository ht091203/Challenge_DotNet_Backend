using Common.Domains.Entities;
using Dapper.Contrib.Extensions;

namespace DotNetTraining.Domains.Entities
{
    [Table("users")]
    public class User : SystemLogEntity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Guid? RoleId { get; set; }
        [Write(false)]
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
