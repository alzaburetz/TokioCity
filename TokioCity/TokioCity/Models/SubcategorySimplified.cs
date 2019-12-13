using System;
using System.Collections.Generic;
using System.Text;

namespace TokioCity.Models
{
    public class SubcategorySimplified
    {
        public int subcat_id { get; set; }
        public string name { get; set; }

        public SubcategorySimplified(int s_id, string _name)
        {
            subcat_id = s_id;
            name = _name;
        }

        public SubcategorySimplified() { }
    }
}
