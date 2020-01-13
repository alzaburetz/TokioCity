using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using TokioCity.Models;
using TokioCity.Extentions;

using System.Threading.Tasks;

namespace TokioCity.ViewModels
{
    public class FavoriteViewModel: BaseViewModel
    {
        public ObservableCollectionFast<AppItem> favorites { get; set; }
        public Command LoadFavorites { get; set; }
        public Command RemoveFavorite { get; set; }
        public Command AddToCart { get; set; }
        bool wasLoaded { get; set; }

        public FavoriteViewModel()
        {
            favorites = new ObservableCollectionFast<AppItem>();
            wasLoaded = false;
            LoadFavorites = new Command(async () =>
            {
                
                    await Task.Run(async () =>
                    {
                        var fav = await DataBase.GetAllStreamAsync<AppItem>("Favorite");
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            favorites.Clear();
                            favorites.AddRange(fav);
                        });
                    });
            });

            RemoveFavorite = new Command((item) =>
            {
                favorites.Remove(item as AppItem);
                DataBase.RemoveItem<AppItem>("Favorite", LiteDB.Query.Where("uid", x => x.AsString == (item as AppItem).uid));
                (item as AppItem).Favorite = false;
                DataBase.UpdateItem<AppItem>("Items", null, (item as AppItem));
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
