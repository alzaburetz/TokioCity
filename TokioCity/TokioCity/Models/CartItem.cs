using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using System.ComponentModel;

namespace TokioCity.Models
{
    public class CartItem: INotifyPropertyChanged
    {
        [BsonIndex]
        public int Id { get; set; }
        private int count;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }
        public AppItem Item { get; set; }
        public List<AppItem> Toppings { get; set; }

        public CartItem()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public CartItem(AppItem _item, IReadOnlyList<AppItem> _toppings, int _count)
        {
            Count = _count;
            Item = _item;
            Toppings = _toppings as List<AppItem>;
        }
    }
}
