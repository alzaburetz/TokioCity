using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TokioCity.Models
{
    [Serializable]
    public class Category
    {
        [LiteDB.BsonId]
        public int Id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string shape { get; set; }
        public int specialItems { get; set; }
        public string image { get; set; }
        public int sort_order { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int count { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string alert { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [LiteDB.BsonField("subcategories")]
        public List<Subcategory> subcategories;
        public Category()
        {
            subcategories = new List<Subcategory>();
        }
    }
}
