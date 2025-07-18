using Application.Settings;
using Common.Controllers;
using Common.Loggers.Interfaces;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraining.Controllers.v1
{
    [Route("api/order-items")]
    [ApiController]
    [Authorize]
    public class OrderItemController : BaseV1Controller<OrderItemService, ApplicationSetting>
    {
        private readonly OrderItemService _service;
        private readonly ILogManager _logger;

        public OrderItemController(IServiceProvider services, IHttpContextAccessor accessor)
            : base(services, accessor)
        {
            _service = services.GetService<OrderItemService>()!;
            _logger = services.GetService<ILogManager>()!;
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(Guid orderId)
        {
            _logger.Info($"[GET] Get order items by orderId: {orderId}", "ORDER_ITEM");
            try
            {
                var items = await _service.GetByOrderId(orderId);
                return Success(items);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get order items by orderId", "ORDER_ITEM");
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách sản phẩm theo đơn hàng", detail = ex.Message });
            }
        }

        [HttpGet("{orderItemId}")]
        public async Task<IActionResult> GetOrderItemById(Guid orderItemId)
        {
            _logger.Info($"[GET] Get order item by ID: {orderItemId}", "ORDER_ITEM");
            try
            {
                var item = await _service.GetOrderItemById(orderItemId);
                if (item == null)
                {
                    _logger.Warn($"Order item not found: {orderItemId}", "ORDER_ITEM");
                    return NotFound(new { message = "Không tìm thấy sản phẩm trong đơn hàng" });
                }

                return Success(item);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed to get order item {orderItemId}", "ORDER_ITEM");
                return StatusCode(500, new { message = "Lỗi khi lấy sản phẩm theo ID", detail = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemDto dto)
        {
            _logger.Info("[POST] Create order item", "ORDER_ITEM");
            if (!ModelState.IsValid)
            {
                _logger.Warn("Invalid order item creation model", "ORDER_ITEM");
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            try
            {
                var result = await _service.CreateOrderItem(dto);
                _logger.Info($"Created order item: {result?.Id}", "ORDER_ITEM");
                return CreatedSuccess(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to create order item", "ORDER_ITEM");
                return StatusCode(500, new { message = "Lỗi khi tạo sản phẩm trong đơn hàng", detail = ex.Message });
            }
        }

        [HttpPut("{orderItemId}")]
        public async Task<IActionResult> UpdateOrderItem(Guid orderItemId, [FromBody] OrderItemDto dto)
        {
            _logger.Info($"[PUT] Update order item: {orderItemId}", "ORDER_ITEM");

            if (!ModelState.IsValid)
            {
                _logger.Warn("Invalid order item update model", "ORDER_ITEM");
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            try
            {
                var updated = await _service.UpdateOrderItem(orderItemId, dto);
                if (updated == null)
                {
                    _logger.Warn($"Order item not found for update: {orderItemId}", "ORDER_ITEM");
                    return NotFound(new { message = "Không tìm thấy sản phẩm để cập nhật" });
                }

                _logger.Info($"Updated order item: {orderItemId}", "ORDER_ITEM");
                return Success(updated);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed to update order item {orderItemId}", "ORDER_ITEM");
                return StatusCode(500, new { message = "Lỗi khi cập nhật sản phẩm trong đơn hàng", detail = ex.Message });
            }
        }

        [HttpDelete("{orderItemId}")]
        public async Task<IActionResult> DeleteOrderItem(Guid orderItemId)
        {
            _logger.Info($"[DELETE] Delete order item: {orderItemId}", "ORDER_ITEM");
            try
            {
                var existing = await _service.GetOrderItemById(orderItemId);
                if (existing == null)
                {
                    _logger.Warn($"Order item not found for deletion: {orderItemId}", "ORDER_ITEM");
                    return NotFound(new { message = "Không tìm thấy sản phẩm để xoá" });
                }

                await _service.DeleteOrderItem(orderItemId);
                _logger.Info($"Deleted order item: {orderItemId}", "ORDER_ITEM");
                return Success("Xoá thành công");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed to delete order item {orderItemId}", "ORDER_ITEM");
                return StatusCode(500, new { message = "Lỗi khi xoá sản phẩm trong đơn hàng", detail = ex.Message });
            }
        }
    }
}
