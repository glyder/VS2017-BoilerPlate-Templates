using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

using static System.Data.CommandType;

using Dapper.Data.API.App.Entities;
using System.Threading.Tasks;

namespace Dapper.Data.API.App.Repository
{
    public class CustomerRepository : BaseRepository, IGenericRepository<Customer>
    {
        public void Add(Customer entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerName", entity.CustomerName);
                parameters.Add("@CustomerEmail", entity.CustomerEmail);
                parameters.Add("@CustomerMobile", entity.CustomerMobile);
                SqlMapper.Execute(con, "AddCustomer", param: parameters, commandType: StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", id);
            SqlMapper.Execute(con, "DeleteCustomer", param: parameters, commandType: StoredProcedure);
        }

        public IEnumerable<Customer> Get()
        {
            IList<Customer> customerList = SqlMapper.Query<Customer>(con, "GetAllCustomer", commandType: StoredProcedure).ToList();
            return customerList;
        }

        public Customer Get(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerID", id);
            return SqlMapper.Query<Customer>((SqlConnection)con,
                "GetCustomerById", parameters, commandType: StoredProcedure).FirstOrDefault();

        }

        public async Task<Customer> GetAsync(int id)
        {
            var empty = await new Task<Customer>(() => null);
            return empty;
        }


        public void Update(Customer entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerID", entity.CustomerName);
            parameters.Add("@CustomerName", entity.CustomerName);
            parameters.Add("@CustomerEmail", entity.CustomerEmail);
            parameters.Add("@CustomerMobile", entity.CustomerMobile);
            SqlMapper.Execute(con, "UpdateCustomer", param: parameters, commandType: StoredProcedure);
        }
    }
}
