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
using CarouselView.FormsPlugin.Abstractions;
using System.Threading.Tasks;

namespace TokioCity.ViewModels
{
    public class BurgersViewModel: BaseViewModel
    {
        public ObservableCollection<AppItem> burgers { get; set; }
        public ObservableCollection<AppItem> toppings { get; set; }
        public ICommand AddToFavorite
        {
            get
            {
                return new Command((item) =>
                {
                    var t = item.GetType();
                });
            }
            set
            {
                AddToFavorite = value;
            }
        }
        public Command LoadBurgers { get; set; }
        public Command LoadToppings { get; set; }

        public BurgersViewModel()
        {
            burgers = new ObservableCollection<AppItem>();
            toppings = new ObservableCollection<AppItem>();
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
