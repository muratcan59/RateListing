using System;
using System.ComponentModel.DataAnnotations;

namespace RateListing.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int? UpperCategoryId { get; set; }
        public string Url { get; set; }
        public int Level { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
