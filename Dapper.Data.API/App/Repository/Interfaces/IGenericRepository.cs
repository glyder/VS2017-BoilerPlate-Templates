using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Data.API.App.Repository
{
    public interface IGenericRepository<T> where T : class {

        IEnumerable<T> Get();
        T Get(int id);
        Task<T> GetAsync(int Id);
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
    }  
}
