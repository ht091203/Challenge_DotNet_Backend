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

<<<<<<< HEAD
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

=======
        [HttpGet]
        public async Task<IActionResult> GetAll() => Success(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) => Success(await _service.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDto dto)
            => CreatedSuccess(await _service.Create(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderDto dto)
            => Success(await _service.Update(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return Success("Deleted");
        }
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
    }
}
