using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Data.API.App.Entities;

using static System.Data.CommandType;

namespace Dapper.Data.API.App.Repository
{

    //http://www.joesauve.com/async-dapper-and-async-sql-connection-management/

    public class UserTestRepository : BaseRepository, IGenericRepository<User>
    {
        public UserTestRepository(string connectionString) : base(connectionString) { }

        public async Task<User> GetPersonById(Guid Id)
        {
            return await WithConnection(async c =>
            {

                // Here's all the same data access code, albeit now it's async, and nicely wrapped in this handy WithConnection() call.
                var p = new DynamicParameters();
                p.Add("Id", Id, DbType.Guid);
                var people = await c.QueryAsync<User>(
                    //sql: "sp_Person_GetById",
                    sql: "GetUserById",
                    param: p,
                    commandType: CommandType.StoredProcedure);

                return people.FirstOrDefault();

            });
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Get()
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(int id)
        {
            var empty = await new Task<User>(() => null);
            return empty;
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
