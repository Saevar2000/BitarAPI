using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lightning.Models
{
    public class Info
    {
        public string id { get; set; }
        public int port { get; set; }
        public List<Address> address { get; set; }
        public List<Address> binding { get; set; }
        public string version { get; set; }
        public int blockheight { get; set; }
        public string network { get; set; }
    }
}