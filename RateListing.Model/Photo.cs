using System.ComponentModel.DataAnnotations.Schema;

namespace RateListing.Model
{
    public class Photo : Base
    {
        public string UserId { get; set; }
        public string Picture { get; set; }
        public int OrderNo { get; set; }

    }
}
