using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using LiteDB;

using TokioCity.Models;
using TokioCity.Services;

namespace TokioCity.ViewModels
{
    public class ProfileViewModel: BaseViewModel
    {
        public ObservableCollection<Offer> Offers { get; set; }
        public ObservableCollection<string> tabs { get; set; }

        public string SelectedItem { get; set; }
        public Command LoadOffers { get; set; }

        public ProfileViewModel(System.Net.Http.HttpClient client)
        {
            Offers = new ObservableCollection<Offer>();
            tabs = new ObservableCollection<string>()
            {
                "БОНУСЫ",
                "ЛИЧНОЕ",
                "ЗАКАЗЫ",
                "АДРЕСА ДОСТАВКИ"
            };
            SelectedItem = tabs[0];

            LoadOffers = new Command(async () =>
            {
                Offers.Clear();
                var offrs = await RequestHelper.GetData<Dictionary<string, Offer>>(client, "/data/app_offers.php?version=");
                foreach (var offer in offrs.Values)
                {
                    Offers.Add(offer);
                }
            });
        }
    }
}
