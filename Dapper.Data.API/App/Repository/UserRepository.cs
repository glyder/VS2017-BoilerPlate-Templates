using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Data.API.App.Entities;

using static System.Data.CommandType;

namespace Dapper.Data.API.App.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public bool AddUser(User user)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", user.UserName);
                parameters.Add("@UserMobile", user.UserMobile);
                parameters.Add("@UserEmail", user.UserEmail);
                parameters.Add("@FaceBookUrl", user.FaceBookUrl);
                parameters.Add("@LinkedInUrl", user.LinkedInUrl);
                parameters.Add("@TwitterUrl", user.TwitterUrl);
                parameters.Add("@PersonalWebUrl", user.PersonalWebUrl);
                SqlMapper.Execute(con, "AddUser", param: parameters, commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Execute a command without returning anything
        public bool DeleteUser(int userId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            SqlMapper.Execute(con, "DeleteUser", param: parameters, commandType: CommandType.StoredProcedure);
            return true;
        }

        //1
        //public IList<User> GetAllUser()
        //{
        //    //UpdateMultipleUsers();
        //    IList<User> customerList = SqlMapper.Query<User>(con, "GetAllUsers", commandType: StoredProcedure).ToList();
        //    return customerList;
        //}

        //2  -Mapping executed result to strongly Type List.
        //public IList<dynamic> GetAllUser() => SqlMapper.Query<dynamic>(con, "GetAllUsers", commandType: StoredProcedure).ToList();

        //3  - Mapping executed result to dynamic objects. 
        //public IList<User> GetAllUser() => SqlMapper.Query<User>(con, "GetAllUsers", commandType: StoredProcedure).ToList();

        //4 - OUR NEW ONE
        public IList<User> GetAllUser() => SqlMapper.Query<User>(con, "GetAllUsers", commandType: StoredProcedure).ToList();

        public User GetUserById(int userId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerID", userId);
                return SqlMapper.Query<User>((SqlConnection)con, "GetUserById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            string Connectionstring = "Data Source=.\\SQLExpress;Initial Catalog=DapperTest;Integrated Security=True";
            using (System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(Connectionstring))
            {
                await sqlConnection.OpenAsync();

                var sqlM = @"SELECT * from threads order by activities desc";
                var resultM = await sqlConnection.QueryAsync<User>(sqlM);

                return resultM.FirstOrDefault();
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", user.UserId);
                parameters.Add("@UserName", user.UserName);
                parameters.Add("@UserMobile", user.UserMobile);
                parameters.Add("@UserEmail", user.UserEmail);
                parameters.Add("@FaceBookUrl", user.FaceBookUrl);
                parameters.Add("@LinkedInUrl", user.LinkedInUrl);
                parameters.Add("@TwitterUrl", user.TwitterUrl);
                parameters.Add("@PersonalWebUrl", user.PersonalWebUrl);
                SqlMapper.Execute(con, "UpdateUser", param: parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //================================


        // Execute a command multiple times with Dapper.
        //=============================================
        public void InsertMultipleUsers()
        {
            object myObj = new[] {
                new { name = "George Pagotelis", email = "george@outlook.com" },
                new { name = "Fred Basset", email = "fred.basset@outlook.com" },
                new { name = "Joe Bloggs", email = "joe.bloggs@outlook.com" }};

            con.Execute(@"insert Users(UserName, UserEmail) values (@name, @email)", myObj);
        }


        // Mapping executed result to strongly Type List.
        //=============================================
        //public IList<Pr> GetAllUsers()
        //{
        //    IList<User> customerList = SqlMapper.Query<User>(con, "GetAllUsers", commandType: StoredProcedure).ToList();
        //    return customerList;
        //}

        // Mapping executed result to dynamic objects. 
        //=============================================
        // public IList<dynamic> GetAllUser() => SqlMapper.Query<dynamic>(con, "GetAllUsers", commandType: StoredProcedure).ToList();


        // Select Multiple Results
        // ========================
        (List<Customer> customers, List<User> users) GetUsersAndCustomers()
        {
            using (var multi = con.QueryMultiple("select * from Customers;select * from Users"))
            {
                var customers = multi.Read<Customer>().ToList();
                var users = multi.Read<User>().ToList();
                return (customers, users);
            }
        }



        #region "Experimental"

        //public UserRepository(string connectionString) : base(connectionString) { }

        //public async Task<User> GetPersonById(Guid Id)
        //{
        //    return await WithConnection(async c => {

        //        // Here's all the same data access code, albeit now it's async, and nicely wrapped in this handy WithConnection() call.
        //        var p = new DynamicParameters();
        //        p.Add("Id", Id, DbType.Guid);
        //        var people = await c.QueryAsync<User>(
        //            //sql: "sp_Person_GetById",
        //            sql: "GetUserById",
        //            param: p,
        //            commandType: CommandType.StoredProcedure);

        //        return people.FirstOrDefault();

        //    });
        //}

        #endregion



    }
}
