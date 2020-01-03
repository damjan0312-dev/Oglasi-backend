using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cassandra;
using AdServiceApplication.Providers;
using AdServiceApplication.Models;

namespace AdServiceApplication.Services
{
    public class CategoryService
    {
        public static List<Category> GetAll()
        {
            ISession session = SessionManager.GetSession();

            List<Category> categories = new List<Category>();

            if (session == null)
            {
                return null;
            }

            var categoryData = session.Execute("Select * From \"category_by_name\"");

            foreach (var category in categoryData)
            {
                Category newCategory = new Category();
                newCategory.id = category["id"] != null ? category["id"].ToString() : string.Empty;
                newCategory.name = category["name"] != null ? category["name"].ToString() : string.Empty;
                newCategory.picture = category["picture"] != null ? category["picture"].ToString() : string.Empty;

                categories.Add(newCategory);
            }

            return categories;
        }
    }
}