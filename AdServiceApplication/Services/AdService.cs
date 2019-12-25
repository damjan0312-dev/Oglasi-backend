using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cassandra;
using AdServiceApplication.Providers;
using AdServiceApplication.Models;

namespace AdServiceApplication.Services
{
    public class AdService
    {
        public static bool AddAd(Ad newAd)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                return false;
            }

            RowSet adData = session.Execute("Insert into \"ads\" (\"id\",headline,description,picture,category,contact,city,price)"+
                " values("+newAd.id+",'"+newAd.headline+ "','"+newAd.description+ "','"+newAd.picture+ "','"+newAd.category+ "','"+newAd.contact+ "','"+newAd.city+ "',"+newAd.price+")  ");
            return true;
        }
        public static bool DeleteAd(string adID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                return false;
            }

            RowSet adData = session.Execute("delete from \"ads\" where \"id\" = '" + adID + "'");
            return true;

        }
        public static bool EditAd(Ad editAd)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                return false;
            }

            RowSet adData = session.Execute("update ads set headline='" + editAd.headline + "',"+
                "description='" + editAd.description + "', " +
                "picture='" + editAd.picture + "'," +
                "category='" + editAd.category + "'," +
                "contact='" + editAd.contact + "'," +
                "city='" + editAd.city + "'," +
                "price='" + editAd.price + "'" +
                " where id="+ editAd.id + "");

            return true;
        }

      

        public static List<Ad> GetAds()
        {
            ISession session = SessionManager.GetSession();

            List<Ad> ads = new List<Ad>();

            if (session == null)
            {
                return null;
            }

            var adData = session.Execute("Select * From \"ads\"");

            foreach(var ad in adData)
            {
                Ad newAd = new Ad();
                newAd.id = ad["id"] != null ? ad["id"].ToString() : string.Empty;
                newAd.headline = ad["headline"] != null ? ad["headline"].ToString() : string.Empty;
                newAd.description = ad["description"] != null ? ad["description"].ToString() : string.Empty;
                newAd.picture = ad["picture"] != null ? ad["picture"].ToString() : string.Empty;
                newAd.category = ad["category"] != null ? ad["category"].ToString() : string.Empty;
                newAd.contact = ad["contact"] != null ? ad["contact"].ToString() : string.Empty;
                newAd.city = ad["city"] != null ? ad["city"].ToString() : string.Empty;
                newAd.price = ad["price"] != null ? Convert.ToDecimal( ad["price"].ToString()) : -1;

                ads.Add(newAd);
            }

            return ads;

        }

        

    }
}