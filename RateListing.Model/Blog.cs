using System.Web.Mvc;

namespace RateListing.Model
{
    public class Blog : Base
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string Summary { get; set; }
        public string Url { get; set; }
    }
}
