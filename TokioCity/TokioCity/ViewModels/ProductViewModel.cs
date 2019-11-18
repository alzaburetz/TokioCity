using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using TokioCity.Models;
using TokioCity.Views;
using TokioCity.Services;
using System.Net.Http;

using LiteDB;
using System.IO;

namespace TokioCity.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public Command LoadProductCommand { get; set; }

        public Command LoadToppingsComand { get; set; }
        public Command AddToppingCommand { get; set; }
        public Command RemoveToppingCommand { get; set; }
        public Command IncreaseTopping { get; set; }
        public Command AddToCart { get; set; }

        public ObservableCollection<AppItem> toppings { get; set; }
        public ObservableCollection<AppItem> selectedToppings { get; set; }
        public AppItem product { get; set; }
        public string prodIndex { get; set; }
        public int amount { get; set; }

        public int width { get; set; }
        public int height { get; set; }

        public ProductViewModel()
        {
            product = new AppItem();
            toppings = new ObservableCollection<AppItem>();
            selectedToppings = new ObservableCollection<AppItem>();
            width = App.screenWidth / 4;
            height = App.screenHeight;
            amount = 1;
            AddToCart = new Command(() =>
            {
                CartItem item = new CartItem(this.product, this.selectedToppings, amount);
                item.Toppings = new List<AppItem>();
                foreach (var topping in this.selectedToppings)
                {
                    item.Toppings.Add(topping);
                }
                var cart = DataBase.GetAllStream<CartItem>("Cart");
                var ie = cart.GetEnumerator();
                while (ie.MoveNext())
                {
                    if (ie.Current.Item.uid == this.product.uid)
                    {
                        ie.Current.Count++;
                        DataBase.UpdateItem<CartItem>("Cart", null, ie.Current);
                        return;
                    }
                }
                DataBase.WriteItem<CartItem>("Cart", item);
                //foreach (CartItem it in cart)
                //{
                //    if (it.Item.uid == this.product.uid)
                //    {
                //        it.Count++;
                //        DataBase.UpdateItem<CartItem>("Cart", null, it);
                //        return;
                //    }
                //}
                //DataBase.WriteItem<CartItem>("Cart", item);
                //else
                //{
                //    if ((check.Item == this.product && check.Toppings == this.selectedToppings.GetEnumerator()) || check.Item.uid == item.Item.uid)
                //    {
                //        item.Count += check.Count;
                //        DataBase.UpdateItem<CartItem>("Cart", null, item);
                //    }
                //    else
                //    {
                //        DataBase.WriteItem<CartItem>("Cart", item);
                //    }
                //}
                   
            });
            LoadProductCommand = new Command((index) =>
            {
                if (index == null) return;
                product = DataBase.GetProduct<AppItem>("Items", index as string);
                if (product.toppings.Count > 1)
                {
                    LoadToppingsComand.Execute(product.toppings);
                }
            });
            LoadToppingsComand = new Command((indecies) =>
            {
                toppings.Clear();
                foreach (var index in indecies as List<string>)
                {
                    var topping = DataBase.GetProduct<AppItem>("Items", index as string);
                    toppings.Add(topping);
                }
            });
            AddToppingCommand = new Command((item) =>
            {
                (item as AppItem).Amount = 1;
                selectedToppings.Add(item as AppItem);
            });
            RemoveToppingCommand = new Command((item) =>
            {
                selectedToppings.Remove(item as AppItem);
            });
            IncreaseTopping = new Command(() =>
            {
                Console.WriteLine("1");
            });
        }

        public ProductViewModel(string uid) : base()
        {
            product = DataBase.GetProduct<AppItem>("Items", uid);
            toppings = new ObservableCollection<AppItem>();
            selectedToppings = new ObservableCollection<AppItem>();
            width = App.screenWidth / 4;
            height = App.screenHeight;
            amount = 1;
            AddToCart = new Command(() =>
            {
                CartItem item = new CartItem(this.product, this.selectedToppings, amount);
                var check = DataBase.GetProduct<CartItem>("Cart", this.product.uid);
                if (check == null)
                {
                    DataBase.WriteItem<CartItem>("Cart", item);
                }
                else
                {
                    if ((check.Item == this.product && check.Toppings == this.selectedToppings.GetEnumerator()) || check.Item.uid == item.Item.uid)
                    {
                        item.Count += check.Count;
                        DataBase.UpdateItem<CartItem>("Cart", null, item);
                    }
                    else
                    {
                        DataBase.WriteItem<CartItem>("Cart", item);
                    }
                }

            });
            LoadProductCommand = new Command((index) =>
            {
                product = DataBase.GetProduct<AppItem>("Items", index as string);
                if (product.toppings.Count > 1)
                {
                    LoadToppingsComand.Execute(product.toppings);
                }
            });
            LoadToppingsComand = new Command((indecies) =>
            {
                toppings.Clear();
                foreach (var index in indecies as List<string>)
                {
                    var topping = DataBase.GetProduct<AppItem>("Items", index as string);
                    toppings.Add(topping);
                }
            });
            AddToppingCommand = new Command((item) =>
            {
                (item as AppItem).Amount = 1;
                selectedToppings.Add(item as AppItem);
            });
            RemoveToppingCommand = new Command((item) =>
            {
                selectedToppings.Remove(item as AppItem);
            });
            IncreaseTopping = new Command(() =>
            {
                Console.WriteLine("1");
            });
        }

    }
}