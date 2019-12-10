using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

using TokioCity.Models;
using TokioCity.Services;

using Xamarin.Forms;

namespace TokioCity.ViewModels.ProfileViewModels
{
    public class OfferViewModel:BaseViewModel
    {
        public ObservableCollection<Offer> Offers { get; set; }
        public Command LoadOffers { get; set; }

        public OfferViewModel(System.Net.Http.HttpClient client)
        {
            Offers = new ObservableCollection<Offer>();
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
