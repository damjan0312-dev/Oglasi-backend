using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdServiceApplication.Models
{
    public class Category
    {
        public string id;
        public string name;
        public string picture;

        public Category() { }
        public Category(string id, string name, string picture)
        {
            this.id = id;
            this.name = name;
            this.picture = picture;
        }
    }
}