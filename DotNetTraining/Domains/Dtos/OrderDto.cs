using System.ComponentModel.DataAnnotations;

namespace DotNetTraining.Domains.Dtos
{
    public class OrderDto
    {
        [Required(ErrorMessage = "Id không được để trống")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "UserId không được để trống")]
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Tổng giá trị đơn hàng không hợp lệ")]
        public decimal TotalAmount { get; set; }
    }
}
