using System.Data;
using Application.Settings;
using AutoMapper;
using Common.Application.CustomAttributes;
using Common.Application.Exceptions;
using Common.Services;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Repositories;

namespace DotNetTraining.Services
{
    [ScopedService]
    public class OrderItemService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection) : BaseService(services)
    {
        private readonly OrderItemRepository _repo = new(connection);

        public async Task<List<OrderItemDto>> GetByOrderId(Guid orderId)
            => _mapper.Map<List<OrderItemDto>>(await _repo.GetByOrderId(orderId));

        public async Task<OrderItemDto?> GetById(Guid id)
        {
            var item = await _repo.GetById(id);
            return item == null ? null : _mapper.Map<OrderItemDto>(item);
        }

        public async Task<OrderItem?> Create(OrderItemDto dto)
            => await _repo.Create(_mapper.Map<OrderItem>(dto));

        public async Task<OrderItem?> Update(Guid id, OrderItemDto dto)
        {
            var existing = await _repo.GetById(id) ?? throw new Exception("not found");
            return await _repo.Update(_mapper.Map(dto, existing));
        }

        public async Task Delete(Guid id)
        {
            var item = await _repo.GetById(id) ?? throw new Exception("not found");
            await _repo.Delete(item);
        }
    }

}
