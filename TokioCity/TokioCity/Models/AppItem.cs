using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using LiteDB.Shell;
using System.ComponentModel;

namespace TokioCity.Models
{
    [Serializable]
    public class AppItem: INotifyPropertyChanged
    {
        public int Id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> flags { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string component { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string uid { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string iiko_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int calories { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<int> category { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string basecategory { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int price2
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged("price2");
            }
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int price { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int price1 { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string href { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string anons { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int sale { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long sort_order { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> cross_sale_items { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string image { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> site { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> toppings { get; set; }
        [JsonProperty("max-toppings")]
        public int max_toppings { get; set; }
        [JsonProperty("component-count")]
        public int component_count { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int carbohydrates { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int fat { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int protein { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ingredients_rus { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ingredients_lat { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int weight1 { get; set; }
        private bool _selected;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChanged("selected");
            }
        }
        private uint _amount;
        public uint Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }
        private int _price;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
