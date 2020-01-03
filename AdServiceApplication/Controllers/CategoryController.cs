using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdServiceApplication.Models;
using AdServiceApplication.Services;

namespace AdServiceApplication.Controllers
{
    public class CategoryController : ApiController
    {

        public List<Category> getCategories()
        {
            return CategoryService.GetAll();
        }
    }
}
