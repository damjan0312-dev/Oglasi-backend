using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdServiceApplication.Models
{
    public class User
    {
        public string id;
        public string name;
        public string lastName;
        public string email;
        public string password;

        public User() { }
        public User(string id, string name, string lastName, string email, string password)
        {
            this.id = id;
            this.name = name;
            this.lastName = lastName;
            this.email = email;
            this.password = password;

        }
    }
}