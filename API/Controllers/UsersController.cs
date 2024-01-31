using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using static Services.Functions.UsersService;

namespace API.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        [Route("api/users/select")]
        public DataTable Select()
        {
            return SelectUsers();
        }


        [HttpGet]
        [Route("api/users/find/{id}")]
        public DataTable Find(string username)
        {
            return FindUser(username);
        }

        [HttpPost]
        [Route("api/users/insert")]
        public string Insert([FromBody] Users newUser)
        {
            return InsertUser(newUser);
        }

        [HttpPut]
        [Route("api/users/update/{id}")]
        public string Update([FromBody] Users editUser)
        {
            return UpdateUser(editUser);
        }

        [HttpDelete]
        [Route("api/users/delete/{id}")]
        public string Delete(string username)
        {
            return DeleteUser(username);
        }
    }
}
