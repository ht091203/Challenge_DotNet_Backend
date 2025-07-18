using Application.Settings;
using Common.Controllers;
using Common.Loggers.Interfaces;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraining.Controllers.v1
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoryController : BaseV1Controller<CategoryService, ApplicationSetting>
    {
        private readonly CategoryService _service;
        private readonly ILogManager _logger;

        public CategoryController(IServiceProvider services, IHttpContextAccessor accessor) : base(services, accessor)
        {
            _service = services.GetService<CategoryService>()!;
            _logger = services.GetRequiredService<ILogManager>();
        }

        [HttpGet("getAllCategory")]
        public async Task<IActionResult> GetAllCategories()
        {
            _logger.Info("[GET] Get all categories", "CATEGORY");
            try
            {
                var categories = await _service.GetAllCategories();
                _logger.Info($"Returned {categories.Count} categories", "CATEGORY");
                return Success(categories);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get all categories", "CATEGORY");
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách danh mục", detail = ex.Message });
            }
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoriesById(Guid categoryId)
        {
            _logger.Info($"[GET] Get category by ID: {categoryId}", "CATEGORY");
            try
            {
                var category = await _service.GetCategoriesById(categoryId);
                if (category == null)
                {
                    _logger.Warn($"Category not found: {categoryId}", "CATEGORY");
                    return NotFound(new { message = "Không tìm thấy danh mục này" });
                }

                return Success(category);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to get category", "CATEGORY");
                return StatusCode(500, new { message = "Lỗi khi truy vấn danh mục", detail = ex.Message });
            }
        }

        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategories([FromBody] CategoryDto dto)
        {
            _logger.Info("[POST] Create category", "CATEGORY");
            if (!ModelState.IsValid)
            {
                _logger.Warn("Invalid model state for category creation", "CATEGORY");
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            try
            {
                var created = await _service.CreateCategories(dto);
                _logger.Info($"Created category: {created?.Id}", "CATEGORY");
                return CreatedSuccess(created);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to create category", "CATEGORY");
                return StatusCode(500, new { message = "Lỗi khi tạo danh mục", detail = ex.Message });
            }
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategories(Guid categoryId, [FromBody] CategoryDto dto)
        {
            _logger.Info($"[PUT] Update category: {categoryId}", "CATEGORY");
            if (!ModelState.IsValid)
            {
                _logger.Warn("Invalid model state for category update", "CATEGORY");
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors = ModelState });
            }

            try
            {
                var updated = await _service.UpdateCategories(categoryId, dto);
                if (updated == null)
                {
                    _logger.Warn($"Category not found for update: {categoryId}", "CATEGORY");
                    return NotFound(new { message = "Không tìm thấy danh mục để cập nhật" });
                }

                _logger.Info($"Updated category: {categoryId}", "CATEGORY");
                return Success(updated);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to update category", "CATEGORY");
                return StatusCode(500, new { message = "Lỗi khi cập nhật danh mục", detail = ex.Message });
            }
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategories(Guid categoryId)
        {
            _logger.Info($"[DELETE] Delete category: {categoryId}", "CATEGORY");
            try
            {
                var existing = await _service.GetCategoriesById(categoryId);
                if (existing == null)
                {
                    _logger.Warn($"Category not found for delete: {categoryId}", "CATEGORY");
                    return NotFound(new { message = "Không tìm thấy danh mục để xoá" });
                }

                await _service.DeleteCategories(categoryId);
                _logger.Info($"Deleted category: {categoryId}", "CATEGORY");
                return Success("Xoá thành công");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to delete category", "CATEGORY");
                return StatusCode(500, new { message = "Lỗi khi xoá danh mục", detail = ex.Message });
            }
        }
    }
}
