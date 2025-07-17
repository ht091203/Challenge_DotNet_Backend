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
    public class OrderService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection) : BaseService(services)
    {
        private readonly OrderRepository _repo = new(connection);

        public async Task<List<OrderDto>> GetAllOrder() =>
            _mapper.Map<List<OrderDto>>(await _repo.GetAllOrders());

        public async Task<OrderDto?> GetOrderById(Guid id)
        {
            var order = await _repo.GetOrderById(id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<Order?> CreateOrder(OrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.Id = Guid.NewGuid(); 

            return await _repo.CreateOrder(order);
        }


        public async Task<Order?> UpdateOrder(Guid id, OrderDto dto)
        {
            var existing = await _repo.GetOrderById(id) ?? throw new Exception("not found");
            return await _repo.UpdateOrder(_mapper.Map(dto, existing));
        }

        public async Task DeleteOrder(Guid id)
        {
            var order = await _repo.GetOrderById(id) ?? throw new Exception("not found");
            await _repo.DeleteOrder(order);
        }
    }
}
