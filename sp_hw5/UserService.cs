using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_hw5
{
    public class UserService
    {
        public string _connectionString;
        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateUser(User user)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(@"insert into Users (Name, Age) values (@Name, @Age)", user);
            }
        }
        public async Task DeleteUser(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(@"delete from Users where Id = @id",new { id });
            }
        }
        public async Task UpdateUser(int id, User user)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync(@"update Users set Name = @Name, Age = @Age where Id = @Id", new { id, user });
            }
        }
        public async Task<List<User>> ReadUsers()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var users =  await db.QueryAsync<User>(@"select * from Users");
                return users.ToList();
            }
        }
        public async Task<User> FindUserById(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<User>(@"select * from Users where Id = @id", new { id });
            } 
        }
    }
}
