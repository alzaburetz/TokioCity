using System;
using System.Collections.Generic;
using System.Text;

namespace TokioCity.Models
{
    [Serializable]
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public int specialItems { get; set; }
        public string image { get; set; }
        public int sort_order { get; set; }
    }
}
