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

<<<<<<< HEAD
        public async Task<List<OrderDto>> GetAllOrder() =>
            _mapper.Map<List<OrderDto>>(await _repo.GetAllOrders());

        public async Task<OrderDto?> GetOrderById(Guid id)
=======
        public async Task<List<OrderDto>> GetAll() =>
            _mapper.Map<List<OrderDto>>(await _repo.GetAllOrders());

        public async Task<OrderDto?> GetById(Guid id)
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        {
            var order = await _repo.GetOrderById(id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

<<<<<<< HEAD
        public async Task<Order?> CreateOrder(OrderDto dto)
=======
        public async Task<Order?> Create(OrderDto dto)
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        {
            var order = _mapper.Map<Order>(dto);
            return await _repo.CreateOrder(order);
        }

<<<<<<< HEAD
        public async Task<Order?> UpdateOrder(Guid id, OrderDto dto)
=======
        public async Task<Order?> Update(Guid id, OrderDto dto)
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        {
            var existing = await _repo.GetOrderById(id) ?? throw new Exception("not found");
            return await _repo.UpdateOrder(_mapper.Map(dto, existing));
        }

<<<<<<< HEAD
        public async Task DeleteOrder(Guid id)
=======
        public async Task Delete(Guid id)
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        {
            var order = await _repo.GetOrderById(id) ?? throw new Exception("not found");
            await _repo.DeleteOrder(order);
        }
    }
}
