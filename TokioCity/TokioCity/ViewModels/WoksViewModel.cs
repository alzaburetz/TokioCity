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
using CarouselView.FormsPlugin.Abstractions;
using System.Threading.Tasks;

namespace TokioCity.ViewModels
{
    public class WoksViewModel: BaseViewModel
    {
        public ObservableCollection<AppItem> main { get; set; }
        public ObservableCollection<AppItem> sauce { get; set; }
        public ObservableCollection<AppItem> meat { get; set; }
        public ObservableCollection<AppItem> toppings { get; set; }

        public string[] tabs { get; set; }

        public int mainCateg = 77;
        public int souceCateg = 229;
        public int meatCateg = 230;
        public int toppingsCateg = 231;

        public int fullPrice;

        public int mainPrice;
        public int saucePrice;
        public int meatPrice;
        public int toppingPrice;

        public Command LoadSubcatsCommand { get; set; }
        public Command CreateWok { get; set; }
        
        public MyProduct wok { get; set; }
        public int CalculateFullPrice()
        {
           return mainPrice + saucePrice + meatPrice + toppingPrice;
        }

        public WoksViewModel()
        {
            main = new ObservableCollection<AppItem>();
            sauce = new ObservableCollection<AppItem>();
            meat = new ObservableCollection<AppItem>();
            toppings = new ObservableCollection<AppItem>();
            wok = new MyProduct();
            
            tabs = new string[2] { "СОБРАТЬ ВОК", "МОИ ВОКИ" };
            CreateWok = new Command((product) =>
            {
                DataBase.WriteItem<MyProduct>("Woks", (MyProduct)product as MyProduct);
            });
            LoadSubcatsCommand = new Command(async () =>
            {
                fullPrice = 0;
                mainPrice = 0;
                saucePrice = 0;
                meatPrice = 0;
                toppingPrice = 0;
                var main = DataBase.GetByQueryEnumerable<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains(mainCateg)));
                this.main.Clear();
                while (main.MoveNext())
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                    this.main.Add(main.Current);
                }
                main.Dispose();

                var sauce = DataBase.GetByQueryEnumerable<AppItem>("Items", Query.Where("category", y => y.AsArray.Contains(229)));
                this.sauce.Clear();
                while (sauce.MoveNext())
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                    this.sauce.Add(sauce.Current);
                }
                sauce.Dispose();

                var meat = DataBase.GetByQueryEnumerable<AppItem>("Items", Query.Where("category", z => z.AsArray.Contains(meatCateg)));
                this.meat.Clear();
                while (meat.MoveNext())
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                    this.meat.Add(meat.Current);
                }
                meat.Dispose();

                var toppings = DataBase.GetByQueryEnumerable<AppItem>("Items", Query.Where("category", w => w.AsArray.Contains(toppingsCateg)));
                this.toppings.Clear();
                while (toppings.MoveNext())
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                    this.toppings.Add(toppings.Current);
                }
                toppings.Dispose();
            });
        }
    }
}
