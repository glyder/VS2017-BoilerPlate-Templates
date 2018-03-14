using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper.Data.API.App.Entities;

using static System.Data.CommandType;
using System.Threading.Tasks;

namespace Dapper.Data.API.App.Repository
{

    //public class ProductMap : EntityMap<Product>
    //{
    //    public ProductMap()
    //    {
    //        // Map property 'Name' to column 'strName'.
    //        Map(p => p.Name)
    //            .ToColumn("strName");

    //        // Ignore the 'LastModified' property when mapping.
    //        Map(p => p.LastModified)
    //            .Ignore();
    //    }
    //}

    public class ProductRepository : BaseRepository, IGenericRepository<Product>
    {
        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        // dbo.Products         public class Product
        // ==========================================
        // ProductID      ->    Id {get; set;}
        // ProductName    ->    Name
        // ProductPrice   ->    Price { get; set; }


        public IEnumerable<Product> Get()
        {
            var columnMap = new ColumnMap();
            columnMap.Add("Id", "ProductId");
            columnMap.Add("Name", "ProductName");
            columnMap.Add("Price", "ProductPrice");
            SqlMapper.SetTypeMap(typeof(Product),
                new CustomPropertyTypeMap(typeof(Product), (type, columnName) => type.GetProperty(columnMap[columnName])));

            List<Product> products = SqlMapper.Query<Product>(
                (SqlConnection)con, "select * from Products", commandType: Text).ToList();

            return products;
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetAsync(int id)
        {
            var empty = await new Task<Product>(() => null);
            return empty;
        }


        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
