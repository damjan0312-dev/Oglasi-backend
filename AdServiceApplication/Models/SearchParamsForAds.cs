using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdServiceApplication.Models
{
    public class SearchParamsForAds
    {
        public string category { get; set; }
        public string city { get; set; }
        public string priceFrom { get; set; }
        public string priceTo { get; set; }

    }
}