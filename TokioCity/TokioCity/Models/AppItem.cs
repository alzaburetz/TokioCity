using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TokioCity.Models
{
    [Serializable]
    public class AppItem
    {
        public List<string> flags { get; set; }
        public string component { get; set; }
        public int uid { get; set; }
        public string iiko_id { get; set; }
        public string name { get; set; }
        public int calories { get; set; }
        public List<int> category { get; set; }
        public string basecategory { get; set; }
        public int price2 { get; set; }
        public int price { get; set; }
        public int price1 { get; set; }
        public string href { get; set; }
        public string anons { get; set; }
        public int sale { get; set; }
        public int sort_order { get; set; }
        public List<dynamic> cross_sale_items { get; set; }
        public string image { get; set; }
        public List<string> site { get; set; }
        public List<string> toppings { get; set; }
        [JsonProperty("max-toppings")]
        public int max_toppings {get; set; }
        [JsonProperty("component-count")]
        public int component_count { get; set; }
        public int carbohydrates { get; set; }
        public int fat { get; set; }
        public int protein { get; set; }
        public string ingredients_rus { get; set; }
        public string ingredients_lat { get; set; }
        public int weight { get; set; }
    }
}
