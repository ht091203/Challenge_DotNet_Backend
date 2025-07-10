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
            return Success("Deleted");
        }
    }
}
