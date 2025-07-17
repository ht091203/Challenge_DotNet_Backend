using Common.Domains.Entities;

namespace DotNetTraining.Domains.Entities
{
    public class UserRole : SystemLogEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
