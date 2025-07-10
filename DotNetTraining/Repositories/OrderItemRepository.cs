using Common.Databases;
using Common.Repositories;
using Dapper;
using DotNetTraining.Domains.Entities;
using System.Data;

namespace DotNetTraining.Repositories
{
    public class OrderItemRepository(IDbConnection connection) : SimpleCrudRepository<OrderItem, Guid>(connection)
    {
        public async Task<List<OrderItem>> GetByOrderId(Guid orderId)
        {
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<OrderItem>(new { OrderId = orderId });
            return (await connection.QueryAsync<OrderItem>(sql, new { OrderId = orderId })).ToList();
        }

        public async Task<OrderItem?> GetById(Guid id)
        {
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<OrderItem>(new { Id = id });
            return await GetOneByConditionAsync(sql, new { Id = id });
        }

        public async Task<OrderItem?> Create(OrderItem item) => await CreateAsync(item);
        public async Task<OrderItem?> Update(OrderItem item) => await UpdateAsync(item);
        public async Task Delete(OrderItem item) => await DeleteAsync(item);
    }
}
