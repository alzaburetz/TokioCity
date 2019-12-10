using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels.ProfileViewModels;

namespace TokioCity.Views.ProfileViews.ProfileComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Offers : ContentView
    {
        OfferViewModel viewModel { get; set; }
        public Offers()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.tokyo-city.ru");
            BindingContext = viewModel = new OfferViewModel(client);
            viewModel.LoadOffers.Execute(null);
            InitializeComponent();
        }
    }
}