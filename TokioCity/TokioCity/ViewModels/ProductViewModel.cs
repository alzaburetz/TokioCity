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