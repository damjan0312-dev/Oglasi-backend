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
        public static string AddAd(Ad newAd)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                return string.Empty;
            }
            try
            {
                RowSet adData = session.Execute("Insert into \"ads_by_category\" (\"id\",headline,description,picture,category,contact,city,price,userid)" +
                    " values(now(),'" + newAd.headline + "','" + newAd.description + "','" + newAd.picture + "','" + newAd.category + "','" + newAd.contact + "','" + newAd.city + "'," + newAd.price + "," + newAd.userId + ")  ");

                adData = session.Execute("select id from ads_by_category where category='" + newAd.category + "' ORDER BY id DESC LIMIT 1");

                var ad = adData.GetRows().First();

                return ad["id"].ToString();
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }
        public static bool DeleteAd(string adID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                return false;
            }

            RowSet adData = session.Execute("delete from \"ads_by_category\" where \"id\" = '" + adID + "'");
            return true;

        }
        public static bool EditAd(Ad editAd)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                return false;
            }

            RowSet adData = session.Execute("update ads_by_category set headline='" + editAd.headline + "'," +
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

            var adData = session.Execute("Select * From \"ads_by_category\"");

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
                newAd.userId = ad["userid"] != null ? ad["userid"].ToString() : string.Empty;

                ads.Add(newAd);
            }

            return ads;

        }

        

    }
}