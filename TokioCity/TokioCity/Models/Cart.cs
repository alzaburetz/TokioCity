using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace TokioCity.Models
{
    public class Cart: INotifyPropertyChanged
    {
        [LiteDB.BsonId]
        public int Id { get; set; }
        public ObservableCollection<CartItem> items { get; set; }
        private int people;
        public int People
        {
            get
            {
                return people;
            }
            set
            {
                people = value;
                OnPropertyChanged("People");
            }
        }
        private int fullcost;
        public int FullCost
        {
            get
            {
                return fullcost;
            }
            set
            {
                fullcost = value;
                OnPropertyChanged("FullCost");
            }
        }
        public Cart()
        {
            people = 1;
            items = new ObservableCollection<CartItem>();
        }
        public Cart(List<CartItem> items, int personas):this()
        {
            foreach (var item in items)
            {
                this.items.Add(item);
            }
            People = personas;
        }
        public void CalculateFullPrice()
        {
            foreach (var item in this.items)
            {
                item.CalculateCost();
                this.FullCost += item.Cost;
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
