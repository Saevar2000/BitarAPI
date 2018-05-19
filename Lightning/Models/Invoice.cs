using System.Collections.Generic;

namespace Lightning.Models
{
    public class Invoice
    {
        public string label { get; set; }
        public string bolt11 { get; set; }
        public string payment_hash { get; set; }
        public int msatoshi { get; set; }
        public string status { get; set; }
        public int expiry_time { get; set; }
        public int expires_at { get; set; }
    }
}