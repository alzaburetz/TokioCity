using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using TokioCity.Models;
using TokioCity.Views;
using TokioCity.Services;
using System.Net.Http;
using System.Windows.Input;

using LiteDB;
using System.IO;
using System.Threading.Tasks;

namespace TokioCity.ViewModels
{
    public class BurgersViewModel: BaseViewModel
    {
        public ObservableCollection<AppItem> burgers { get; set; }
        public ObservableCollection<AppItem> toppings { get; set; }
        public Command AddToFavorite { get; set; }
        public Command LoadBurgers { get; set; }
        public Command LoadToppings { get; set; }
        public Command AddToCart { get; set; }

        public int height { get; set; }

        public BurgersViewModel()
        {
            burgers = new ObservableCollection<AppItem>();
            toppings = new ObservableCollection<AppItem>();
            height = App.screenHeight / 2;
            AddToFavorite = new Command((item) =>
            {
                var check = DataBase.GetProduct<AppItem>("Favorite", (item as AppItem).uid);
                if (check == null)
                {
                    DataBase.WriteItem<AppItem>("Favorite", (AppItem)item as AppItem);
                    (item as AppItem).Favorite = true;
                    DataBase.UpdateItem<AppItem>("Items", null, (item as AppItem));
                }
                else
                {
                    DataBase.RemoveItem<AppItem>("Favorite", Query.Where("uid", x => x.AsString == check.uid));
                    (item as AppItem).Favorite = false;
                    DataBase.UpdateItem<AppItem>("Items", null, (item as AppItem));
                }
            });
            LoadBurgers = new Command(() =>
            {
                var burgers = DataBase.GetByQueryEnumerable<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains(222)));
                this.burgers.Clear();
                while (burgers.MoveNext())
                {
                    this.burgers.Add(burgers.Current);
                }
                burgers.Dispose();
                LoadToppings.Execute(null);
            });

            AddToCart = new Command((item) =>
            {
                var Item = (AppItem)item as AppItem;
                var cart = DataBase.GetAllStream<CartItem>("Cart");
                CartItem cartItem = new CartItem(Item, new List<AppItem>(), 1);
                var ie = cart.GetEnumerator();
                while (ie.MoveNext())
                {
                    if (ie.Current.Item != null && ie.Current.Item.uid == Item.uid)
                    {
                        ie.Current.Count++;
                        DataBase.UpdateItem<CartItem>("Cart", null, ie.Current);
                        return;
                    }
                }
                DataBase.WriteItem<CartItem>("Cart", cartItem);
            });


            LoadToppings = new Command(() =>
            {
                var toppings = DataBase.GetByQueryEnumerable<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains(1564)));
                this.toppings.Clear();
                while (toppings.MoveNext())
                {
                    this.toppings.Add(toppings.Current);
                }
                toppings.Dispose();
            }); 
        }
    }
}
