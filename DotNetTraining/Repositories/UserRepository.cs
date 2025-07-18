using System.Data;
using BPMaster.Domains.Entities;
using Common.Databases;
using Common.Repositories;
using Dapper;
using Dapper.Contrib.Extensions;
using DotNetTraining.Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DotNetTraining.Repositories
{
    public class UserRepository(IDbConnection connection) : SimpleCrudRepository<User, Guid>(connection)
    {
        public async Task<List<User>> GetAllUsers()
        {
            var sql = SqlCommandHelper.GetSelectSql<User>();
            var result = await connection.QueryAsync<User>(sql);
            return result.ToList();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<User>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            try
            {
                var sql = "SELECT * FROM users WHERE LOWER(\"Email\") = LOWER(@Email)";
                return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in GetUserByEmail: {e.Message}");
                return null;
            }
        }

        public async Task<User?> Create(User user)
        {
            return await CreateAsync(user);
        }

        public async Task<User?> UpdateUser(User user)
        {
            return await UpdateAsync(user);
        }

        public async Task DeleteUser(User user)
        {
            await DeleteAsync(user);
        }

        public async Task<IList<string>> GetUserRoles(Guid userId)
        {
            var sql = @"
                    SELECT r.""Name""
                    FROM user_roles ur
                    JOIN roles r ON ur.""RoleId"" = r.""Id""
                    WHERE ur.""UserId"" = @UserId";
            var roles = await connection.QueryAsync<string>(sql, new { UserId = userId });
            return roles.ToList();
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            var sql = @"SELECT * FROM roles WHERE LOWER(""Name"") = LOWER(@Name)";
            return await connection.QueryFirstOrDefaultAsync<Role>(sql, new { Name = roleName });
        }


        public async Task AssignRoleToUserAsync(Guid userId, Guid roleId)
        {
            var sql = @"INSERT INTO user_roles(""UserId"", ""RoleId"") VALUES(@UserId, @RoleId)";
            await connection.ExecuteAsync(sql, new { UserId = userId, RoleId = roleId });
        }


        internal async Task AssignRoleToUserAsync(UserRole userRole)
        {
            var sql = @"INSERT INTO user_roles(""UserId"", ""RoleId"") VALUES(@UserId, @RoleId)";
            await connection.ExecuteAsync(sql, new
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId
            });
        }
        public async Task<Role?> GetRoleByIdAsync(Guid roleId)
        {
            var sql = @"SELECT * FROM roles WHERE ""Id"" = @Id";
            return await connection.QueryFirstOrDefaultAsync<Role>(sql, new { Id = roleId });
        }
    }
}
