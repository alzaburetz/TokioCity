using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using TokioCity.Models;

using TokioCity.Services;
using System.Net.Http;
using System.Collections.ObjectModel;
using TokioCity.ViewModels;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Categories : ContentPage
    {
        private HttpClient client;
        CategoriesViewModel viewModel;
        public Categories()
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("https://www.tokyo-city.ru");
            BindingContext = viewModel = new CategoriesViewModel(client);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadCategoriesCommand.Execute(null);
        }
    }
}