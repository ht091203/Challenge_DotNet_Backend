using Common.Repositories;
using Dapper;
using DotNetTraining.Domains.Entities;
using System.Data;

namespace DotNetTraining.Repositories
{
    public class ProductRepository(IDbConnection connection) : SimpleCrudRepository<Product, Guid>(connection)
    {
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            try
            {
                var sql = "SELECT Name, Description, Price FROM products";
                return await connection.QueryAsync<Product>(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in GetAllProduct: {e.Message}");
                return Enumerable.Empty<Product>();
            }
        }

        public async Task<Product?> GetProductById(Guid productId)
        {
            try
            {
                var sql = "SELECT * FROM products WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = productId });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in GetProductById: {e.Message}");
                return null;
            }
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            try
            {
                var sql = "UPDATE products SET Name = @Name, Price = @Price, Description = @Description WHERE Id = @Id RETURNING *";
                return await connection.QueryFirstOrDefaultAsync<Product>(sql, new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in UpdateProduct: {e.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            try
            {
                var sql = "DELETE FROM products WHERE Id = @Id";
                var rowsAffected = await connection.ExecuteAsync(sql, new { Id = productId });
                return rowsAffected > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in DeleteProduct: {e.Message}");
                return false;
            }
        }

        public async Task<Product?> CreateProduct(Product product)
        {
            try
            {
                var sql = "INSERT INTO products (Name, Price, Description) VALUES (@Name, @Price, @Description) RETURNING *";
                return await connection.QuerySingleOrDefaultAsync<Product>(sql, product);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in CreateProduct: {e.Message}");
                return null;
            }
        }

        public async Task<Product?> GetProductByName(string name)
        {
            try
            {
                var sql = "SELECT * FROM products WHERE Name = @Name";
                return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Name = name });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in GetProductByName: {e.Message}");
                return null;
            }
        }
    }
}
