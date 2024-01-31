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
        public DataTable Find(string id)
        {
            return FindUser(id);
        }

        [HttpPost]
        [Route("api/users/insert")]
        public string Insert([FromBody] Users nuevo)
        {
            return InsertUsers(nuevo);
        }

        [HttpPut]
        [Route("api/users/update/{id}")]
        public string Update([FromBody] Users nuevo)
        {
            return UpdateUsers(nuevo);
        }

        [HttpDelete]
        [Route("api/users/delete/{id}")]
        public string Delete(string id)
        {
            return DeleteUsers(id);
        }
    }
}
