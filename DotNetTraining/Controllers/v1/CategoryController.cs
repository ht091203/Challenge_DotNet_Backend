using Application.Settings;
using Common.Controllers;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraining.Controllers.v1
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : BaseV1Controller<CategoryService, ApplicationSetting>
    {
        private readonly CategoryService _service;
        public CategoryController(IServiceProvider services, IHttpContextAccessor accessor) : base(services, accessor)
            => _service = services.GetService<CategoryService>()!;

<<<<<<< HEAD
        [HttpGet("getAllCategory")]
        public async Task<IActionResult> GetAllCategories()
        {
            var category = await _service.GetAllCategories();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Success(category);
        }

        // GET: api/users/{userId}
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoriesById(Guid categoryId)
        {
            var category = await _service.GetCategoriesById(categoryId);
            if (category == null)
            {
                return NotFound(new { message = "Không tìm thấy danh mục này" });
            }
            return Success(category);
        }
        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategories([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return CreatedSuccess(await _service.CreateCategories(dto));
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategories(Guid id, [FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Success(await _service.UpdateCategories(id, dto));
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategories(Guid id)
        {
            await _service.DeleteCategories(id);
=======
        [HttpGet]
        public async Task<IActionResult> GetAll() => Success(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) => Success(await _service.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
            => CreatedSuccess(await _service.Create(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryDto dto)
            => Success(await _service.Update(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
            return Success("Deleted");
        }
    }
}
