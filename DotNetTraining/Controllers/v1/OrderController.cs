using Application.Settings;
using BPMaster.Services;
using Common.Controllers;
using Common.Loggers.Interfaces;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraining.Controllers.v1
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : BaseV1Controller<OrderService, ApplicationSetting>
    {
        private readonly OrderService _service;
        private readonly ILogManager _logger;

        public OrderController(IServiceProvider services, IHttpContextAccessor accessor)
            : base(services, accessor)
        {
            _service = services.GetService<OrderService>()!;
            _logger = services.GetService<ILogManager>()!;
        }

        [HttpGet("getAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            _logger.Info("[GET] Get all orders", "ORDER");
            try
            {
                var orders = await _service.GetAllOrder();
                _logger.Info($"Returned {orders.Count} orders", "ORDER");
                return Success(orders);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get all orders", "ORDER");
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách đơn hàng", detail = ex.Message });
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            _logger.Info($"[GET] Get order by ID: {orderId}", "ORDER");
            try
            {
                var order = await _service.GetOrderById(orderId);
                if (order == null)
                {
                    _logger.Warn($"Order not found: {orderId}", "ORDER");
                    return NotFound(new { message = "Không tìm thấy đơn hàng này" });
                }

                return Success(order);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed to get order {orderId}", "ORDER");
                return StatusCode(500, new { message = "Lỗi khi lấy đơn hàng", detail = ex.Message });
            }
        }

        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto dto)
        {
            _logger.Info("[POST] Create order", "ORDER");
            if (!ModelState.IsValid)
            {
                _logger.Warn("Invalid order creation model", "ORDER");
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            try
            {
                var created = await _service.CreateOrder(dto);
                _logger.Info($"Order created: {created?.Id}", "ORDER");
                return CreatedSuccess(created);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to create order", "ORDER");
                return StatusCode(500, new { message = "Lỗi khi tạo đơn hàng", detail = ex.Message });
            }
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] OrderDto dto)
        {
            _logger.Info($"[PUT] Update order: {orderId}", "ORDER");

            if (!ModelState.IsValid)
            {
                _logger.Warn("Invalid order update model", "ORDER");
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            try
            {
                var updated = await _service.UpdateOrder(orderId, dto);
                if (updated == null)
                {
                    _logger.Warn($"Order not found for update: {orderId}", "ORDER");
                    return NotFound(new { message = "Không tìm thấy đơn hàng để cập nhật" });
                }

                _logger.Info($"Updated order: {orderId}", "ORDER");
                return Success(updated);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed to update order {orderId}", "ORDER");
                return StatusCode(500, new { message = "Lỗi khi cập nhật đơn hàng", detail = ex.Message });
            }
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            _logger.Info($"[DELETE] Delete order: {orderId}", "ORDER");
            try
            {
                var existing = await _service.GetOrderById(orderId);
                if (existing == null)
                {
                    _logger.Warn($"Order not found for deletion: {orderId}", "ORDER");
                    return NotFound(new { message = "Không tìm thấy đơn hàng để xoá" });
                }

                await _service.DeleteOrder(orderId);
                _logger.Info($"Deleted order: {orderId}", "ORDER");
                return Success("Xoá thành công");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Failed to delete order {orderId}", "ORDER");
                return StatusCode(500, new { message = "Lỗi khi xoá đơn hàng", detail = ex.Message });
            }
        }
    }
}
