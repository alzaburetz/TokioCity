using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using TokioCity.Models;
using TokioCity.Views;
using TokioCity.Services;
using TokioCity.Extentions;
using System.Net.Http;
using System.Windows.Input;

using LiteDB;
using System.IO;
using System.Threading.Tasks;

namespace TokioCity.ViewModels
{
    public class BurgersViewModel: BaseViewModel
    {
        public ObservableCollectionFast<AppItem> burgers { get; set; }
        public ObservableCollectionFast<AppItem> toppings { get; set; }
        public Command AddToFavorite { get; set; }
        public Command LoadBurgers { get; set; }
        public Command LoadToppings { get; set; }
        public Command AddToCart { get; set; }

        public int height { get; set; }
        public int widthGrid { get; set; }
        bool wasLoaded { get; set; }

        public BurgersViewModel()
        {
            wasLoaded = false;
            burgers = new ObservableCollectionFast<AppItem>();
            var info = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo;
            widthGrid = (int)(info.Width / info.Density);
            toppings = new ObservableCollectionFast<AppItem>();
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
            LoadBurgers = new Command(async () =>
            {
                if (wasLoaded)
                {
                    return;
                }
                else
                {
                    await Task.Run(async () =>
                    {
                        var burgers = await DataBase.GetByQueryEnumerableAsync<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains(222)));
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            this.burgers.Clear();
                            this.burgers.AddRange(burgers);
                        });

                        LoadToppings.Execute(null);
                    });
                    wasLoaded = true;
                }
                
            });

            AddToCart = new Command((item) =>
            {
                var Item = (AppItem)item as AppItem;
                Task.Run(() =>
                {
                    AddToCartMethod(Item);
                }).ContinueWith((obj) =>
                {
                    TokioCity.Views.CategoriesTabs.RenewCount();
                });

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

        private void AddToCartMethod(AppItem Item)
        {

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
        }
    }
}
