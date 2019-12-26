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
        public void AddAd([FromBody] Ad newAd)
        {
            AdService.AddAd(newAd);
        }
    }
}
