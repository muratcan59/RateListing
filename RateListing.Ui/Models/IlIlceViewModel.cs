using RateListing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateListing.Ui.Models
{
    public class IlIlceViewModel
    {
        public int? CityNum { get; set; }
        public int? DistrictNum { get; set; }

        public virtual City Cities { get; set; }

        public virtual District Districts { get; set; }
    }
}