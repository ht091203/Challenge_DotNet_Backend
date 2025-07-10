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
    }
}
