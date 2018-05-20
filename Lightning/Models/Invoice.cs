using System;

namespace Lightning.Models
{
    public class Invoice
    {
        public string bolt11 { get; set; }
        public string description { get; set; }
        public string label { get; set; }
        public string payment_hash { get; set; }
        public string status { get; set; }
        public int? expires_at { get; set; }
        public int? expiry_time { get; set; }
        public int? msatoshi { get; set; }
    }
}