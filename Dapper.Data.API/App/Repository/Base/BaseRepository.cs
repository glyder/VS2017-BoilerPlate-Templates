using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Data.API.App.Repository
{
    public class BaseRepository
    {
        protected IDbConnection con;

        public BaseRepository()
        {
            string connectionString = "Data Source=.\\SQLExpress;Initial Catalog=DapperTest;Integrated Security=True";
            _ConnectionString = connectionString;

            con = new SqlConnection(connectionString);
        }


        public void Dispose()
        {
            //throw new NotImplementedException();  
        }



        #region "Experimental"

        private readonly string _ConnectionString;

        public BaseRepository(string con)
        {
            _ConnectionString = con;
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    await connection.OpenAsync(); // Asynchronously open a connection to the database
                    return await getData(connection); // Asynchronously execute getData, which has been passed in as a Func<IDBConnection, Task<T>>
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        #endregion
    }
}
