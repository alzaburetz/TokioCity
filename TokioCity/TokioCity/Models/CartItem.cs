using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

namespace TokioCity.Models
{
    public class CartItem
    {
        [BsonIndex]
        public int Id { get; set; }
        public int Count { get; set; }
        public AppItem Item { get; set; }
        public List<AppItem> Toppings { get; set; }

        public CartItem()
        {

        }

        public CartItem(AppItem _item, IReadOnlyList<AppItem> _toppings, int _count)
        {
            Count = _count;
            Item = _item;
            Toppings = _toppings as List<AppItem>;
        }
    }
}
