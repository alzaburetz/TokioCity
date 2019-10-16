using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TokioCity.Models
{
    [Serializable]
    public class Subcategory
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shape { get; set; }
        public int specialItems { get; set; }
        public string image { get; set; }
        public int sort_order { get; set; }
        public int count { get; set; }
        public string alert { get; set; }
        [JsonProperty("max-toppings")]
        public int max_toppings { get; set; }
        [JsonProperty("min-toppings")]
        public int min_toppings { get; set; }
    }
}
