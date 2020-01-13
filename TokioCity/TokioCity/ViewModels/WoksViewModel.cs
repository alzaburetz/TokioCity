using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using TokioCity.Models;
using TokioCity.Extentions;
using TokioCity.Views;
using TokioCity.Services;
using System.Net.Http;

using LiteDB;
using System.IO;
using System.Threading.Tasks;

namespace TokioCity.ViewModels
{
    public class WoksViewModel: BaseViewModel
    {
        public ObservableCollectionFast<AppItem> main { get; set; }
        public ObservableCollectionFast<AppItem> sauce { get; set; }
        public ObservableCollectionFast<AppItem> meat { get; set; }
        public ObservableCollectionFast<AppItem> toppings { get; set; }

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
            main = new ObservableCollectionFast<AppItem>();
            sauce = new ObservableCollectionFast<AppItem>();
            meat = new ObservableCollectionFast<AppItem>();
            toppings = new ObservableCollectionFast<AppItem>();
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
                var main = await DataBase.GetByQueryEnumerableAsync<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains(mainCateg)));
                this.main.Clear();
                this.main.AddRange(main);
                var sauce = await DataBase.GetByQueryEnumerableAsync<AppItem>("Items", Query.Where("category", y => y.AsArray.Contains(229)));
                this.sauce.Clear();
                this.sauce.AddRange(sauce);
                var meat = await DataBase.GetByQueryEnumerableAsync<AppItem>("Items", Query.Where("category", z => z.AsArray.Contains(meatCateg)));
                this.meat.Clear();
                this.meat.AddRange(meat);
                var toppings = await DataBase.GetByQueryEnumerableAsync<AppItem>("Items", Query.Where("category", w => w.AsArray.Contains(toppingsCateg)));
                this.toppings.AddRange(toppings);
            });
        }
    }
}
