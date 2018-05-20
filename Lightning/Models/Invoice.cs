using System;
using Newtonsoft.Json;

namespace Lightning.Models
{
    public class Invoice
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string bolt11 { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string label { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string payment_hash { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? expires_at { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? expiry_time { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? msatoshi { get; set; }
    }
}