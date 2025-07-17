using Application.Settings;
using BPMaster.Services;
using Common.Controllers;
using Common.Loggers.Interfaces;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Services;
using iText.Commons.Actions.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

[Route("api/product")]
[ApiController]
//[Authorize]
public class ProductController : BaseV1Controller<ProductService, ApplicationSetting>
{
    private readonly ProductService _productService;
    private readonly ILogManager _logger;

    public ProductController(IServiceProvider services, IHttpContextAccessor httpContextAccessor)
        : base(services, httpContextAccessor)
    {
        _productService = services.GetService<ProductService>()!;
        _logger = services.GetService<ILogManager>()!;
    }

    [HttpGet("getAllProduct")]
    public async Task<IActionResult> GetAllProduct()
    {
        _logger.Info("[GET] Get all products", "PRODUCT");
        try
        {
            var product = await _productService.GetAllProduct();
            _logger.Info($"Returned {product.Count} products", "PRODUCT");
            return Success(product);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to get all products", "PRODUCT");
            return StatusCode(500, new { message = "Lỗi hệ thống khi lấy danh sách sản phẩm", detail = ex.Message });
        }
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        _logger.Info($"[GET] Get product by ID: {productId}", "PRODUCT");
        try
        {
            var product = await _productService.GetProductById(productId);
            if (product == null)
            {
                _logger.Warn($"Product not found: {productId}", "PRODUCT");
                return NotFound(new { message = "Không tìm thấy sản phẩm" });
            }

            return Success(product);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to get product {productId}", "PRODUCT");
            return StatusCode(500, new { message = "Lỗi khi truy vấn sản phẩm", detail = ex.Message });
        }
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(Guid categoryId)
    {
        _logger.Info($"[GET] Get products by category: {categoryId}", "PRODUCT");
        try
        {
            var products = await _service.GetByCategoryId(categoryId);
            _logger.Info($"Returned {products.Count} products by category", "PRODUCT");
            return Success(products);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to get products by category", "PRODUCT");
            return StatusCode(500, new { message = "Lỗi khi lấy sản phẩm theo danh mục", detail = ex.Message });
        }
    }

    [HttpPost("createProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto dto)
    {
        _logger.Info($"[POST] Create product: {dto.Name}", "PRODUCT");

        if (!ModelState.IsValid)
        {
            _logger.Warn("Invalid product creation model", "PRODUCT");
            return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });
        }

        try
        {
            var created = await _productService.CreateProduct(dto);
            _logger.Info($"Created product: {created?.Id}", "PRODUCT");
            return CreatedSuccess(created);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to create product", "PRODUCT");
            return StatusCode(500, new { message = "Lỗi khi tạo sản phẩm", detail = ex.Message });
        }
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        _logger.Info($"[DELETE] Delete product: {productId}", "PRODUCT");
        try
        {
            var existing = await _productService.GetProductById(productId);
            if (existing == null)
            {
                _logger.Warn($"Product not found for deletion: {productId}", "PRODUCT");
                return NotFound(new { message = "Không tìm thấy sản phẩm để xoá" });
            }

            await _productService.DeleteProduct(productId);
            _logger.Info($"Deleted product: {productId}", "PRODUCT");
            return Success("Xoá thành công");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to delete product {productId}", "PRODUCT");
            return StatusCode(500, new { message = "Lỗi khi xoá sản phẩm", detail = ex.Message });
        }
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] ProductDto productdto)
    {
        _logger.Info($"[PUT] Update product: {productId}", "PRODUCT");

        if (!ModelState.IsValid)
        {
            _logger.Warn("Invalid product update payload", "PRODUCT");
            return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });
        }

        try
        {
            var updated = await _productService.UpdateProduct(productId, productdto);
            if (updated == null)
            {
                _logger.Warn($"Cannot find product to update: {productId}", "PRODUCT");
                return NotFound(new { message = "Không tìm thấy sản phẩm để cập nhật" });
            }

            _logger.Info($"Updated product: {productId}", "PRODUCT");
            return Success(updated);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to update product {productId}", "PRODUCT");
            return StatusCode(500, new { message = "Lỗi khi cập nhật sản phẩm", detail = ex.Message });
        }
    }
}



