using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using TokioCity.Models;

using LiteDB;

namespace TokioCity.ViewModels
{
    public class LunchesViewModel: BaseViewModel
    {
        public ObservableCollection<AppItem> lunches { get; set; }
        public ObservableCollection<AppItem> salads { get; set; }
        public ObservableCollection<AppItem> soups { get; set; }
        public ObservableCollection<AppItem> hotmeal { get; set; }
        public ObservableCollection<AppItem> ramen { get; set; }
        public ObservableCollection<AppItem> pizza { get; set; }
        public ObservableCollection<AppItem> rolls { get; set; }
        public ObservableCollection<AppItem> garnish { get; set; }
        public Command LoadLunchesCommand { get; set; }


        private const int lunchesCategory = 2263;
        private const int saladsCategory = 2264;
        private const int soupsCategory = 2265;
        private const int hotMealCategory = 2266;
        private const int ramenCategory = 2267;
        private const int pizzaCategory = 2268;
        private const int rollsCategory = 2269;
        private const int garnishCategory = 2270;

        public LunchesViewModel()
        {
            lunches = new ObservableCollection<AppItem>();
            salads = new ObservableCollection<AppItem>();
            soups = new ObservableCollection<AppItem>();
            hotmeal = new ObservableCollection<AppItem>();
            ramen = new ObservableCollection<AppItem>();
            pizza = new ObservableCollection<AppItem>();
            rolls = new ObservableCollection<AppItem>();
            garnish = new ObservableCollection<AppItem>();

            LoadLunchesCommand = new Command(() =>
            {
                var salads = DataBase.GetByQueryEnumerable<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains(garnishCategory)));
                while (salads.MoveNext())
                {
                    this.garnish.Add(salads.Current);
                }
            });
        }
    }
}
