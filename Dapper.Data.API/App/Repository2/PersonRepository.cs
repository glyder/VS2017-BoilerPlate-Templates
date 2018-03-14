using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Data.API.App.Entities;

namespace Dapper.Data.API.App.Repository
{

    //https://gist.github.com/jsauve/ffa2f0dc534aee3a3f16

    public class PersonRepository : Base2Repository
    {
        public PersonRepository(string connectionString) : base(connectionString)
        {
        }

        // Assumes you have a Person table in your DB that aligns with the Person POCO model.
        // Assumes you have an exsiting SQL sproc in your DB with @Id UNIQUEIDENTIFIER as a parameter. The sproc returns rows from the Person table.
        public async Task<Person> GetPersonById(Guid Id)
        {
            return await WithConnection(async c =>
            {
                var p = new DynamicParameters();
                p.Add("Id", Id, DbType.Guid);
                var people = await c.QueryAsync<Person>(sql: "sp_Person_GetById", param: p, commandType: CommandType.StoredProcedure);
                return people.FirstOrDefault();
            });
        }
    }
}
