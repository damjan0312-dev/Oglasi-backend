using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdServiceApplication.Models;
using AdServiceApplication.Services;

namespace AdServiceApplication.Controllers
{
    public class UserController : ApiController
    {
        [System.Web.Http.Route("api/user/add")]
        public void AddUser([FromBody] User newUser)
        {
            UserService.AddUser(newUser);
        }

     // [System.Web.Http.Route("api/auth")]
        public User AuthUser([FromUri]string email)
        {
            UserService.AuthUser(email);

            return null; 
        }


      public User getUsers(string email)
      {
          return UserService.AuthUser(email);
      }
    }
}
