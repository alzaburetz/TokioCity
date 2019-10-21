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

        public ObservableCollection<AppItem> toppings { get; set; }
        public AppItem product { get; set; }
        public string prodIndex { get; set; }
        public int amount { get; set; }

        public ProductViewModel()
        {
            product = new AppItem();
            LoadProductCommand = new Command((index) =>
            {
                product = DataBase.GetProduct<AppItem>("Items", index as string);
                
            });
        }


    }
}