using System;
using System.Collections.Generic;
using System.Text;

namespace TokioCity.Models
{
    [Serializable]
    public class Offer
    {
        public string title { get; set; }
        public string image { get; set; }
        public string html { get; set; }
        public string href { get; set; }
        public string item_id { get; set; }
        public int clickable { get; set; }
    }
}
