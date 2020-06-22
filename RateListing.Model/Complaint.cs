using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateListing.Model
{
    public class Complaint : Base
    {
        public string NameSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public bool IsSolve { get; set; }
        public bool IsApprove { get; set; }
        public int Score { get; set; }
        public string Answer { get; set; }
        public bool IsComplaint { get; set; }
    }
}
