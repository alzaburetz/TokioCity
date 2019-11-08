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
    public class WokListViewModel: BaseViewModel
    {
        public Command LoadMyWoks { get; set; }
        public Command RemoveMyWok { get; set; }
        public ObservableCollection<MyProduct> MyWoks { get; set; }
        public WokListViewModel()
        {
            MyWoks = new ObservableCollection<MyProduct>();
            LoadMyWoks = new Command(async () =>
            {
                MyWoks.Clear();
                var data = DataBase.GetAllStream<MyProduct>("Woks");
                var en = data.GetEnumerator();
                try
                {
                    while (en.MoveNext())
                    {
                        int cost = 0;
                        foreach (var item in en.Current.Components)
                        {
                            item.price2 = item.price;
                            item.Amount = 1;
                            cost += item.price2;
                        }
                        en.Current.Cost = cost;
                        MyWoks.Add(en.Current);
                    }
                } catch { }
                
            });
            RemoveMyWok = new Command((Id) =>
            {
                var typ = Id.GetType();
                DataBase.RemoveItem<MyProduct>("Woks", Query.Where("Id", x => x.AsInt32 == (int)Id));
            });
        }
    }
}
