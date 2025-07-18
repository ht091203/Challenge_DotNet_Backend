using Common.Domains.Entities;
using Dapper.Contrib.Extensions;

namespace DotNetTraining.Domains.Entities
{
    [Table("orders")]
    public class Order : SystemLogEntity<Guid>
    {
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
