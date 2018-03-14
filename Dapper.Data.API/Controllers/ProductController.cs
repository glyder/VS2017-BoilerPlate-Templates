using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Dapper.Data.API.App.Entities;
using Dapper.Data.API.App.Repository;

namespace Dapper.Data.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {

        IGenericRepository<Product> _productRepository;
        public ProductController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            //return new string[] { "value1", "value2" };
            return _productRepository.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
