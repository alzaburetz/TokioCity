using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

namespace TokioCity.Models
{
    public class CategorySimplified
    {
        [BsonId]
        public int Id { get; set; }
        //[BsonField("category_id")]
        public int cat_id { get; set; }
        public string image { get; set; }
        public List<SubcategorySimplified> subcats { get; set; }
        public CategorySimplified()
        {
            subcats = new List<SubcategorySimplified>();
        }
        public CategorySimplified(Category cat) : this()
        {
            this.cat_id = cat.id;
            this.image = cat.image;
            foreach (var subcat in cat.subcategories)
            {
                subcats.Add(new SubcategorySimplified(subcat.id, subcat.name));
            }
        }
    }
}
