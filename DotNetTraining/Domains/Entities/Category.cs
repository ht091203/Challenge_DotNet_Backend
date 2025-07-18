using Common.Domains.Entities;
using Dapper.Contrib.Extensions;

namespace DotNetTraining.Domains.Entities
{
    [Table("categories")]
    public class Category : SystemLogEntity<Guid>
    {
        public string Name { get; set; }
    }
}
