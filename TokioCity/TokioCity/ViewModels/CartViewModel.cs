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
        public Cart cartObject { get; set; }

        public Command LoadCart { get; set; }
        public Command AddItem { get; set; }
        public Command ReduceItem { get; set; }
        public Command RemoveFromCart { get; set; }
        public Command IncreasePeople { get; set; }
        public Command ReducePeople { get; set; }
        public CartItem itemToRemove { get; set; }

        public CartViewModel()
        {
            cartObject = new Cart();
            itemToRemove = new CartItem();

            LoadCart = new Command(() =>
            {
                cartObject.items.Clear();
                cartObject.FullCost = 0;
                var data = DataBase.GetAllStream<CartItem>("Cart");
                var ie = data.GetEnumerator();
                while (ie.MoveNext())
                {
                    cartObject.items.Add(ie.Current);
                    ie.Current.CalculateCost();
                    cartObject.FullCost += ie.Current.Cost * ie.Current.Count;
                }
            });
            AddItem = new Command((item) =>
            {
                cartObject.items.First<CartItem>(x => x == item).Count++;
                cartObject.FullCost += (item as CartItem).Cost;
                var update = cartObject.items.First<CartItem>(x => x == item);
                DataBase.UpdateItem<CartItem>("Cart", null, update);
            });
            ReduceItem = new Command((item) =>
            {

                var update = cartObject.items.First<CartItem>(x => x == item);
                if (update.Count > 1)
                {
                    cartObject.items.First<CartItem>(x => x == item).Count--;
                    cartObject.FullCost -= (item as CartItem).Cost;
                    DataBase.UpdateItem<CartItem>("Cart", null, update);
                }
                else
                {
                    cartObject.items.Remove(item as CartItem);
                    cartObject.FullCost -= (item as CartItem).Cost;
                    DataBase.RemoveItem<CartItem>("Cart", query: LiteDB.Query.Where("_id", x => x.AsInt32 == (item as CartItem).Id));
                }
            });
            RemoveFromCart = new Command((item) =>
            {
                if (item == null)
                    item = itemToRemove;
                cartObject.items.Remove(item as CartItem);
                cartObject.FullCost -= (item as CartItem).Cost * (item as CartItem).Count;
                DataBase.RemoveItem<CartItem>("Cart", LiteDB.Query.Where("_id", x => x.AsInt32 == (item as CartItem).Id));
            });


            IncreasePeople = new Command(() => cartObject.People++);
            ReducePeople = new Command(() =>
            {
                if (cartObject.People > 1)
                {
                    cartObject.People--;
                }
            });
        }
    }
}
