using Common.Domains.Entities;
using Dapper.Contrib.Extensions;

namespace DotNetTraining.Domains.Entities
{
    [Table("roles")]
    public class Role : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
