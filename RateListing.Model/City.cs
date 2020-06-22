using System.ComponentModel.DataAnnotations;

namespace RateListing.Model
{
    public class City
    {
        [Key]
        public int CityNum { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
