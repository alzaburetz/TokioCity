using System;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

using TokioCity.Models;

namespace TokioCity.ViewModels
{
    public class FavoriteViewModel: BaseViewModel
    {
        public ObservableCollection<AppItem> favorites { get; set; }
        public Command LoadFavorites { get; set; }
        public Command RemoveFavorite { get; set; }
        public Command AddToCart { get; set; }

        public FavoriteViewModel()
        {
            favorites = new ObservableCollection<AppItem>();
            LoadFavorites = new Command(() =>
            {
                favorites.Clear();
                var fav = DataBase.GetAllStream<AppItem>("Favorite");
                var enumerator = fav.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    favorites.Add(enumerator.Current);
                }
                fav = null;
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
                var cart = DataBase.GetAllStream<CartItem>("Cart");
                CartItem cartItem = new CartItem(Item, new System.Collections.Generic.List<AppItem>(), 1);
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

        }
    }
}
