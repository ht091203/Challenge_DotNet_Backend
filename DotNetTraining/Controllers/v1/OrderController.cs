using System.Text;
using Application.Settings;
using BPMaster.Services;
using Common.Controllers;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Services;
using iText.Commons.Actions.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraining.Controllers.v1
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : BaseV1Controller<OrderService, ApplicationSetting>
    {
        private readonly OrderService _service;
        public OrderController(IServiceProvider services, IHttpContextAccessor accessor) : base(services, accessor)
            => _service = services.GetService<OrderService>()!;

        [HttpGet("getAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            var order = await _service.GetAllOrder();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Success(order);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var order = await _service.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng này" });
            }
            return Success(order);
        }

        [HttpPost("{createOrder}")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return CreatedSuccess(await _service.CreateOrder(dto));
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Success(await _service.UpdateOrder(id, dto));
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _service.DeleteOrder(id);
            return Success("Deleted");
        }

    }
}
