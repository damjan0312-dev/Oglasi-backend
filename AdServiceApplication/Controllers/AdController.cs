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
    public class AdController : ApiController
    {
        //[System.Web.Http.Route("api/ad/get")]
        public IEnumerable<Ad> Get()
        {
            return AdService.GetAds();
        }

        [System.Web.Http.Route("api/ad/add")]
        public string AddAd([FromBody] Ad newAd)
        {
            return AdService.AddAd(newAd);
        }

        [System.Web.Http.Route("api/ad/delete")]
        public bool DeleteAd(string id, string category)
        {
            return AdService.DeleteAd(id, category);
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.Route("api/ad/search")]
        public IEnumerable<Ad> SearchAd(string category, string city, string priceFrom, string priceTo)
        {
            return AdService.SearchAds(category, city, priceFrom, priceTo);
        }
    }
}
