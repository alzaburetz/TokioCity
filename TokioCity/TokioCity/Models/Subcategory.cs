using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TokioCity.Models
{
    [Serializable]
    [LiteDB.BsonField("subcategories")]
    public class Subcategory
    {
        [LiteDB.BsonId]
        public int _id_ { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string shape { get; set; }
        public string icon_layout { get; set; }
        public string icon_size { get; set; }
        public int specialItems { get; set; }
        public string image { get; set; }
        public int sort_order { get; set; }
        public int count { get; set; }
        public string alert { get; set; }
        [JsonProperty("max-toppings")]
        public int max_toppings { get; set; }
        [JsonProperty("min-toppings")]
        public int min_toppings { get; set; }

        public Subcategory()
        {

        }

        public Subcategory(SubcategorySimplified subcat)
        {
            this.id = subcat.subcat_id;
            this.name = subcat.name;
        }
    }
}
