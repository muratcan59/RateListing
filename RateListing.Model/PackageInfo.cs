using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateListing.Model
{
    public class PackageInfo
    {
        public Package package { get; set; }
        public List<string> discountTexts { get; set; }
        public int totalDiscountPercent { get; set; }
        public decimal totalDiscountPrice { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
