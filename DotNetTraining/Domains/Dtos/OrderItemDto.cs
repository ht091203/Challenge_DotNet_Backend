using System.ComponentModel.DataAnnotations;

namespace DotNetTraining.Domains.Dtos
{
    public class OrderItemDto
    {
        [Required(ErrorMessage = "Id không được để trống")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "OrderId không được để trống")]
        public Guid OrderId { get; set; }
        [Required(ErrorMessage = "ProductId không được để trống")]
        public Guid ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn hoặc bằng 0")]
        public decimal UnitPrice { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn hoặc bằng 0")]
        public decimal TotalPrice { get; set; }
    }
}
