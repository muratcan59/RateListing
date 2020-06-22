using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateListing.Model
{
    public class District
    {
        [Key]
        public int DistrictNum { get; set; }
        public string Name { get; set; }
        [ForeignKey("Cities")]
        public int CityNum { get; set; }
        public virtual City Cities { get; set; }
    }
}
