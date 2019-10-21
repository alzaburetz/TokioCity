using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokioCity.Models;
using TokioCity.ViewModels;

namespace TokioCity.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Product : ContentPage
    {
        public ProductViewModel viewModel { get; set; }
        public string i;

        public Product(AppItem item)
        {
            BindingContext = viewModel = new ProductViewModel();
            viewModel.product = item;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public async void GoBack(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        private void Add(object sender, EventArgs args)
        {
            viewModel.amount++;
            Amount.Text = viewModel.amount.ToString();
        }

        private void Reduce(object sender, EventArgs args)
        {
            if (viewModel.amount > 0)
            viewModel.amount--;
            Amount.Text = viewModel.amount.ToString();
        }
    }
}