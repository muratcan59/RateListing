using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RateListing.Model.Enums;

namespace RateListing.Model
{
    public class Time : Base
    {
        public string UserId { get; set; }
        public Gunler Day { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public bool isClosed { get; set; }
    }
}
