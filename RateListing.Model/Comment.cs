using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateListing.Model
{
    public class Comment : Base
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public string UserId { get; set; }
        public bool IsApprove { get; set; }        
    }
}
