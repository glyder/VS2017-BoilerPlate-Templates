using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Data.API.App.Business;
using Dapper.Data.API.App.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dapper.Data.API.Controllers
{

    //TODO TO FINISH
    //https://code.msdn.microsoft.com/Dapper-and-Repository-68710cd7

    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET api/user  
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userManager.GetAllUser();
        }

        // GET api/user/5  
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userManager.GetUserById(id);
        }
        //public async Task<IEnumerable<User>> GetUsersAsync(int id)
        //{
        //    return await _userManager.GetUserByIdAsync(id).First;
        //}


        // POST api/user  
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _userManager.AddUser(user);
        }

        // PUT api/user/5  
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            _userManager.UpdateUser(user);
        }

        // DELETE api/user/5  
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userManager.DeleteUser(id);
        }
    }
}