using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateListing.Model
{
    public class CommentAnswer : Base
    {
        public string Answer { get; set; }
        public string UserId { get; set; }
        public int CommentId { get; set; }
    }
}
