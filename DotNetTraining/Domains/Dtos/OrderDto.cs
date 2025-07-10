<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;

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
=======
﻿namespace DotNetTraining.Domains.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        public decimal TotalAmount { get; set; }
    }
}
