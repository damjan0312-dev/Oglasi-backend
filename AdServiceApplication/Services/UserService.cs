using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdServiceApplication.Models;
using AdServiceApplication.Providers;
using Cassandra;

namespace AdServiceApplication.Services
{
    public class UserService
    {

        public static bool AddUser(User user)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                return false;
            }

            RowSet adData = session.Execute("Insert into \"user\" (\"id\",name, lastname, email, password)" +
                " values(now(),'" + user.name + "','" + user.lastName + "','" + user.email + "','" + user.password + "'");
            return true;
        }

        public static User AuthUser(string email)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                return null;
            }

            var adData = session.Execute("select * from user where email='" + email + "'");

            var u = adData.First();
            User user = new User();
            if (u != null)
            {
               
                user.id = u["id"] != null ? u["id"].ToString() : string.Empty;
                user.name = u["name"] != null ? u["name"].ToString() : string.Empty;
                user.lastName = u["lastname"] != null ? u["lastname"].ToString() : string.Empty;
                user.email = u["email"] != null ? u["email"].ToString() : string.Empty;
                user.password = u["password"] != null ? u["password"].ToString() : string.Empty;

                return user;
            }

            return null;
            

        }
    }
}