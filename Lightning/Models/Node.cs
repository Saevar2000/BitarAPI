using System.Collections.Generic;

namespace Lightning.Models
{
    public class Node
    {
        public string nodeid { get; set; }
        public string alias { get; set; }
        public string color { get; set; }
        public int last_timestamp { get; set; }
        public List<Address> addresses { get; set; }
    }
}