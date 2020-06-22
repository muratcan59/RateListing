using System;
using System.ComponentModel.DataAnnotations;

namespace RateListing.Model
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
