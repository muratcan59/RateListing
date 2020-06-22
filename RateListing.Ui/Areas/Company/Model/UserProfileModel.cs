using RateListing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateListing.Ui.Areas.Company.Model
{
    public class UserProfileModel
    {
        public User user { get; set; }
        public List<Time> times { get; set; }
        public string[] paymentMethods { get; set; }
    }
}