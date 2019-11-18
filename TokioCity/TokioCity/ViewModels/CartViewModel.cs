using System;
using System.Collections.Generic;
using System.Text;

using Xamarin;
using Xamarin.Forms;

using TokioCity.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace TokioCity.ViewModels
{
    public class CartViewModel: BaseViewModel
    {
        public ObservableCollection<CartItem> cart { get; set; }

        public Command LoadCart { get; set; }
        public Command AddItem { get; set; }
        public Command ReduceItem { get; set; }

        public CartViewModel()
        {
            cart = new ObservableCollection<CartItem>();

            LoadCart = new Command(() =>
            {
                cart.Clear();
                var data = DataBase.GetAllStream<CartItem>("Cart");
                var ie = data.GetEnumerator();
                while (ie.MoveNext())
                {
                    cart.Add(ie.Current);
                }
            });
            AddItem = new Command((item) =>
            {
                cart.First<CartItem>(x => x == item).Count++;
                var update = cart.First<CartItem>(x => x == item);
                DataBase.UpdateItem<CartItem>("Cart", null, update);
            });
            ReduceItem = new Command((item) =>
            {

                var update = cart.First<CartItem>(x => x == item);
                if (update.Count > 1)
                {
                    cart.First<CartItem>(x => x == item).Count--;
                    DataBase.UpdateItem<CartItem>("Cart", null, update);
                }
                else
                {
                    cart.Remove(item as CartItem);
                    DataBase.RemoveItem<CartItem>("Cart", query: LiteDB.Query.Where("_id", x => x.AsInt32 == (item as CartItem).Id));
                }
            });
        }
    }
}
