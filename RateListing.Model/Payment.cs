using System;

namespace RateListing.Model
{
    public class Payment : Base
    {
        public string userId { get; set; }
        public string paymentDesc { get; set; }
        public decimal price { get; set; }
        public string iyzipay_paymentId { get; set; }
        public string iyzipay_token { get; set; }
        public bool isSuccess { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public DateTime Date { get; set; }
    }
}
