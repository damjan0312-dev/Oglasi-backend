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
        public IEnumerable<Ad> Get()
        {

            Ad newAd = new Ad("1","ker","ker dobar","slika","kutre","61","Pariz",100);
            AdService.AddAd(newAd);

            return AdService.GetAds();
        }
    }
}
