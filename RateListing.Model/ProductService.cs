using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RateListing.Model
{
    public class ProductService : Base
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
