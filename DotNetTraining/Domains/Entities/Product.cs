using Common.Domains.Entities;
using Dapper.Contrib.Extensions;

namespace DotNetTraining.Domains.Entities
{
    [Table("product")]
    public class Product : SystemLogEntity<Guid>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
