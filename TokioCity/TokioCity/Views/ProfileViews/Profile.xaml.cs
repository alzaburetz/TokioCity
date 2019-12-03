using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        ProfileViewModel viewModel { get; set; }
        public Profile()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.tokyo-city.ru");
            BindingContext = viewModel = new ProfileViewModel(client);

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadOffers.Execute(null);
        }
    }
}