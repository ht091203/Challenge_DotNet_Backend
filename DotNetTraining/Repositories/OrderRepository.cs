using Common.Databases;
using Common.Repositories;
using Dapper;
using DotNetTraining.Domains.Entities;
using System.Data;

namespace DotNetTraining.Repositories
{
    public class OrderRepository(IDbConnection connection) : SimpleCrudRepository<Order, Guid>(connection)
    {
        public async Task<List<Order>> GetAllOrders()
        {
            var sql = SqlCommandHelper.GetSelectSql<Order>();
            return (await connection.QueryAsync<Order>(sql)).ToList();
        }

        public async Task<Order?> GetOrderById(Guid id)
        {
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Order>(new { Id = id });
            return await GetOneByConditionAsync(sql, new { Id = id });
        }

        public async Task<Order?> CreateOrder(Order order) => await CreateAsync(order);
        public async Task<Order?> UpdateOrder(Order order) => await UpdateAsync(order);
        public async Task DeleteOrder(Order order) => await DeleteAsync(order);
    }

}
