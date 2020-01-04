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
        public static bool DeleteAd(string adID, string category)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
            {
                return false;
            }

            TimeUuid id = (TimeUuid)Guid.Parse(adID);
            
           RowSet adData = session.Execute("delete from ads_by_category where category='"+ category +"' AND id=" + id + "");
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


        public static List<Ad> SearchAds(string category, string city, string priceFrom, string priceTo)
        {
            ISession session = SessionManager.GetSession();

            List<Ad> ads = new List<Ad>();

            if (session == null)
            {
                return null;
            }


            if (category != null && city != null && priceFrom != null && priceTo != null)
            {
                decimal priceF = Convert.ToDecimal(priceFrom);
                decimal priceT = Convert.ToDecimal(priceTo);
                var adData = session.Execute("Select * From ads_by_category_city_price where category='"+category+"' and  "+
                    "city='" + city + "' and price>" + priceF + " and price<" + priceT + "  ");
                searchData(ads, adData);
            }
            else if (category != null && city != null && priceFrom != null)
            {
                decimal priceF = Convert.ToDecimal(priceFrom);
                var adData = session.Execute("Select * From ads_by_category_city_price where category='" + category + "' and  " +
                    "city='" + city + "' and price>" + priceF);
                searchData(ads, adData);
            }
            else if (category != null && city != null && priceTo != null)
            {
                decimal priceT = Convert.ToDecimal(priceTo);
                var adData = session.Execute("Select * From ads_by_category_city_price where category='" + category + "' and  " +
                    "city='" + city + "' and price<" + priceTo);
                searchData(ads, adData);
            }
            else if (category != null && city != null)
            {
                var adData = session.Execute("Select * From ads_by_category_city_price where category='" + category + "' and  " +
                    "city='" + city+"'");
                searchData(ads, adData);
            }
            else if (category != null && priceFrom != null && priceTo != null)
            {
                decimal priceT = Convert.ToDecimal(priceTo);
                decimal priceF = Convert.ToDecimal(priceFrom);
                var adData = session.Execute("Select * From ads_by_category_price where category='" + category + "' and  " +
                    "price>" + priceF + " and price<" + priceTo);
                searchData(ads, adData);
            }
            else if (category != null && priceFrom != null)
            {
                decimal priceF = Convert.ToDecimal(priceFrom);
                var adData = session.Execute("Select * From ads_by_category_price where category='" + category + "' and  " +
                    "price>" + priceF);
                searchData(ads, adData);
            }
            else if (category != null && priceTo != null)
            {
                decimal priceT = Convert.ToDecimal(priceTo);
                var adData = session.Execute("Select * From ads_by_category_price where category='" + category + "' and  " +
                    "price<" + priceT);
                searchData(ads, adData);
            }
            else if (city != null && priceFrom != null && priceTo != null)
            {
                decimal priceF = Convert.ToDecimal(priceFrom);
                decimal priceT = Convert.ToDecimal(priceTo);
                var adData = session.Execute("Select * From ads_by_city_price where city='" + city + "' and  " +
                    "price>" + priceF + " and price<" + priceTo);
                searchData(ads, adData);
            }
            else if (city != null && priceFrom != null)
            {
                decimal priceF = Convert.ToDecimal(priceFrom);
                var adData = session.Execute("Select * From ads_by_city_price where city='" + city + "' and  " +
                    "price>" + priceF );
                searchData(ads, adData);
            }
            else if (city != null && priceTo != null)
            {
                decimal priceT = Convert.ToDecimal(priceTo);
                var adData = session.Execute("Select * From ads_by_city_price where city='" + city + "' and  " +
                    "price<" + priceTo);
                searchData(ads, adData);
            }
            else if (city != null)
            {
                var adData = session.Execute("Select * From ads_by_city_price where city='" + city+"'");
                searchData(ads, adData);
            }
            else if (category != null)
            {
                var adData = session.Execute("Select * From ads_by_category where category='" + category+"'");
                searchData(ads, adData);
            }
            else if (priceFrom != null)
            {
                string categories=string.Empty;

                var adCategory = session.Execute("select name from category_by_name");
                foreach (var cat in adCategory)
                {
                    categories +="'"+ cat["name"].ToString() + "',";
                }
                categories=categories.Remove(categories.Length - 1, 1);
                decimal priceF = Convert.ToDecimal(priceFrom);
                var adData = session.Execute("Select * From ads_by_category_price where category in("+categories+") and price>" + priceF);
                searchData(ads, adData);
            }
            else if (priceTo != null)
            {
                decimal priceT = Convert.ToDecimal(priceTo);

                string categories = string.Empty;

                var adCategory = session.Execute("select name from category_by_name");
                foreach (var cat in adCategory)
                {
                    categories += "'" + cat["name"].ToString() + "',";
                }
                categories = categories.Remove(categories.Length - 1, 1);

                var adData = session.Execute("Select * From ads_by_category_price where category in(" + categories + ") and price<" + priceT);
                searchData(ads, adData);
            }

            return ads;

        }

        private static void searchData(List<Ad> ads, RowSet adData)
        {
            foreach (var ad in adData)
            {
                Ad newAd = new Ad();
                newAd.id = ad["id"] != null ? ad["id"].ToString() : string.Empty;
                newAd.headline = ad["headline"] != null ? ad["headline"].ToString() : string.Empty;
                newAd.description = ad["description"] != null ? ad["description"].ToString() : string.Empty;
                newAd.picture = ad["picture"] != null ? ad["picture"].ToString() : string.Empty;
                newAd.category = ad["category"] != null ? ad["category"].ToString() : string.Empty;
                newAd.contact = ad["contact"] != null ? ad["contact"].ToString() : string.Empty;
                newAd.city = ad["city"] != null ? ad["city"].ToString() : string.Empty;
                newAd.price = ad["price"] != null ? Convert.ToDecimal(ad["price"].ToString()) : -1;
                newAd.userId = ad["userid"] != null ? ad["userid"].ToString() : string.Empty;

                ads.Add(newAd);
            }
        }

        

    }
}