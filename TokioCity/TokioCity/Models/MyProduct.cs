using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LiteDB;

namespace TokioCity.Models
{
    public class MyProduct: INotifyPropertyChanged
    {
        [BsonIndex]
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public CartItem ConvertToCartItem(int Count)
        {
            var asAppItem = new AppItem();
            asAppItem.name = this.Name;
            asAppItem.price = this.Cost;
            List<AppItem> toppings = new List<AppItem>();
            foreach (var component in this.Components)
            {
                toppings.Add(component);
            }
            var item = new CartItem(asAppItem, toppings, Count);
            return item;
        }

        public ObservableCollection<AppItem> Components { get; set; }
        private int _cost;
        public int Cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
                OnPropertyChanged("Cost");
            }
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
    }
}
