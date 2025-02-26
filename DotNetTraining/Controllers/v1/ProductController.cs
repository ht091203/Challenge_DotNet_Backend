using System.Text;
using Application.Settings;
using BPMaster.Services;
using Common.Controllers;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/product")]
[ApiController]
public class ProductController : BaseV1Controller<ProductService, ApplicationSetting>
{
    private readonly ProductService _productService;
    public ProductController(
            IServiceProvider services,
            IHttpContextAccessor httpContextAccessor

            )
            : base(services, httpContextAccessor)
    {
        this._productService = services.GetService<ProductService>()!;
    }

    [HttpGet("getAllProduct")]
    public async Task<IActionResult> GetAllProduct()
    {
        try
        {

            var products = await _productService.GetAllProduct();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        return Success(await _productService.GetProductById(productId));
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] Product product)
    {
        try
        {
            var updatedProduct = await _productService.UpdateProduct(productId, product);
            if (updatedProduct == null)
            {
                return NotFound($"User with ID {productId} not found.");
            }
            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        try
        {
            var deleted = await _productService.DeleteProduct(productId);
            if (!deleted)
            {
                return NotFound($"User with ID {productId} not found.");
            }
            return Ok($"User with ID {productId} has been deleted successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto newProduct)
    {
        try
        {
            var createdProduct = await _productService.CreateProduct(newProduct);
            if (createdProduct == null)
            {
                return BadRequest("Failed to create product.");
            }
            return CreatedAtAction(nameof(GetProductById), new { productId = createdProduct.Id }, createdProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);
        }
    }
}


