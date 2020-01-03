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

            RowSet adData = session.Execute("Insert into \"user\" (\"email\", id ,name, lastname, password)" +
                " values('" + user.email + "', now(),'" + user.name + "','" + user.lastName + "','" + user.password + "')");

            if (adData.Count() > 0)
                return true;
            else
                return true;
        }

        public static User AuthUser(string email)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                return null;
            }

            var adData = session.Execute("select * from user_by_email where email='" + email + "'");

             
            User user = new User();

            foreach (var u in adData)
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

        public static User Login(string email, string password)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                return null;
            }

            var adData = session.Execute("select * from user where email='" + email + "' AND password='"+ password+"'");


            User user = new User();

            foreach (var u in adData)
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