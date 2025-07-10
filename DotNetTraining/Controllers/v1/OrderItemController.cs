using Application.Settings;
using Common.Controllers;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraining.Controllers.v1
{
    [Route("api/order-items")]
    [ApiController]
    public class OrderItemController : BaseV1Controller<OrderItemService, ApplicationSetting>
    {
        private readonly OrderItemService _service;
        public OrderItemController(IServiceProvider services, IHttpContextAccessor accessor) : base(services, accessor)
            => _service = services.GetService<OrderItemService>()!;

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(Guid orderId)
            => Success(await _service.GetByOrderId(orderId));

<<<<<<< HEAD
        [HttpGet("{orderItemId}")]
        public async Task<IActionResult> GetOrderItemById(Guid id)
            => Success(await _service.GetOrderItemById(id));

        [HttpPost("{createOrderItem}")]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return CreatedSuccess(await _service.CreateOrderItem(dto));
        }

        [HttpPut("{orderItemId}")]
        public async Task<IActionResult> UpdateOrderItem(Guid id, [FromBody] OrderItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Success(await _service.UpdateOrderItem(id, dto));
        }

        [HttpDelete("{orderItemId}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
        {
            await _service.DeleteOrderItem(id);
            return Success("Deleted");
        }

=======
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
            => Success(await _service.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderItemDto dto)
            => CreatedSuccess(await _service.Create(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderItemDto dto)
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
