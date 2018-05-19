using System.Collections.Generic;

namespace Lightning.Models
{
    public class Peer
    {
        public string state { get; set; }
        public string id { get; set; }
        public string alias { get; set; }
        public string color { get; set; }
        public List<string> netaddr { get; set; }
        public bool connected { get; set; }
        public string owner { get; set; }
    }
}