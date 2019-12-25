using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdServiceApplication.Models
{
    public class Ad
    {
        public string id { get; set; }
        public string headline { get; set; }
        public string description { get; set; }
        public string picture { get; set; }
        public string category { get; set; }
        public string contact { get; set; }
        public string city { get; set; }
        public decimal price { get; set; }

        public Ad()
        {

        }
        public Ad(string id,string headline,string description,string picture,string category,string contact,string city,decimal price)
        {
            this.id = id;
            this.headline = headline;
            this.description = description;
            this.picture = picture;
            this.category = category;
            this.contact = contact;
            this.city = city;
            this.price = price;
        }
    }
}