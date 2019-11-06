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

        public ObservableCollection<AppItem> toppings { get; set; }
        public ObservableCollection<AppItem> selectedToppings { get; set; }
        public AppItem product { get; set; }
        public string prodIndex { get; set; }
        public int amount { get; set; }

        public ProductViewModel()
        {
            product = new AppItem();
            toppings = new ObservableCollection<AppItem>();
            selectedToppings = new ObservableCollection<AppItem>();
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
            AddToppingCommand = new Command((items) =>
            {
                selectedToppings.Clear();
                foreach (var item in items as List<AppItem>)
                {
                    selectedToppings.Add(item);
                }
            });
        }


    }
}