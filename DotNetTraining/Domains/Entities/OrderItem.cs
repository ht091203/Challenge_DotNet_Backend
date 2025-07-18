using Common.Domains.Entities;
using Dapper.Contrib.Extensions;

namespace DotNetTraining.Domains.Entities
{
    [Table("order_items")]
    public class OrderItem : SystemLogEntity<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
