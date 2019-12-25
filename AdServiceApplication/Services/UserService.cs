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
    }
}