using Common.Databases;
using Common.Repositories;
using Dapper;
using DotNetTraining.Domains.Entities;
using System.Data;

namespace DotNetTraining.Repositories
{
    public class CategoryRepository(IDbConnection connection) : SimpleCrudRepository<Category, Guid>(connection)
    {
        public async Task<List<Category>> GetAllCategories()
        {
            var sql = SqlCommandHelper.GetSelectSql<Category>();
            var result = await connection.QueryAsync<Category>(sql);
            return result.ToList();
        }

        public async Task<Category?> GetCategoryById(Guid id)
        {
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Category>(new { Id = id });
            return await GetOneByConditionAsync(sql, new { Id = id });
        }

        public async Task<Category?> CreateCategory(Category category)
            => await CreateAsync(category);

        public async Task<Category?> UpdateCategory(Category category)
            => await UpdateAsync(category);

        public async Task DeleteCategory(Category category)
            => await DeleteAsync(category);
    }

}
