<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;

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
=======
﻿namespace DotNetTraining.Domains.Dtos
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        public decimal TotalPrice { get; set; }
    }
}
