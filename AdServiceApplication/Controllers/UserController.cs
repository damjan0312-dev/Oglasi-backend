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
        public void addUser([FromBody] User newUser)
        {
            UserService.AddUser(newUser);
        }


      public User getUser(string email)
      {
          return UserService.AuthUser(email);
      }

      //[System.Web.Http.Route("api/user/login")]
      public User login([FromBody] User u){
          return UserService.Login(u.email, u.password);
      }
    }
}
