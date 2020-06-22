using RateListing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateListing.Ui.Models
{
    public class CompanyViewModel
    {
        public virtual Comment Comment { get; set; }
        public virtual Complaint Complaint { get; set; }
        public virtual OfferRequest OfferRequest { get; set; }
    }
}