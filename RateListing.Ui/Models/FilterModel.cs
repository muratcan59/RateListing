using RateListing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateListing.Ui.Models
{
    public class FilterModel
    {
        public List<CategoryFilter> Categories { get; set; }
        public int Mesafe { get; set; }
        public int KurumsalRating { get; set; }
        public int MusteriRatingi { get; set; }

        public bool mukemmel { get; set; }
        public bool cokiyi { get; set; }
        public bool iyi { get; set; }
        public bool orta { get; set; }

        public bool yildiz5 { get; set; }
        public bool yildiz4 { get; set; }
        public bool yildiz3 { get; set; }
        public bool yildiz2 { get; set; }
        public bool yildiz1 { get; set; }

        public Location currentLocation { get; set; }
    }

    public class CategoryFilter
    {
        public bool Value { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Location
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}