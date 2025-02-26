using System.Data;
using BPMaster.Domains.Entities;
using Common.Repositories;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using DotNetTraining.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetTraining.Repositories
{
    public class UserRepository(IDbConnection connection) : SimpleCrudRepository<User, Guid>(connection)
    {
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var sql = "SELECT Name, Email FROM users";
                return await connection.QueryAsync<User>(sql);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<User?> GetUserById(Guid userId)
        {
            var sql = "SELECT * FROM users WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = userId });
        }

        public async Task<User?> UpdateUser(User user)
        {
            var sql = "UPDATE users SET Name = @Name, Email = @Email, Password = @Password WHERE Id = @Id RETURNING *";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            });
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var sql = "DELETE FROM users WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = userId });
            return rowsAffected > 0;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var sql = "SELECT * FROM users WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }


        public async Task<User?> CreateUser(User user)
        {
            var sql = "INSERT INTO users (Id, Name, Email, Password) VALUES (@Id, @Name, @Email, @Password) RETURNING *";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, user);
        }


    }
}
