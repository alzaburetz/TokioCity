using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels;
using TokioCity.Models;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Woks : ContentPage
    {
        public WoksViewModel viewModel;
        public Woks()
        {
            BindingContext = viewModel = new WoksViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadSubcatsCommand.Execute(null);
        }

        private void ProductSelected(object sender, SelectionChangedEventArgs args)
        {
            var item = args.CurrentSelection[0] as AppItem;
            
            viewModel.mainPrice = item.price;
            viewModel.fullPrice = viewModel.CalculateFullPrice();
            FullPrice.Text = $"{viewModel.fullPrice}.-";
        }

        private void MeatSelected(object sender, SelectionChangedEventArgs args)
        {
            var item = args.CurrentSelection[0] as AppItem;
            viewModel.meatPrice = item.price;
            viewModel.fullPrice = viewModel.CalculateFullPrice();
            FullPrice.Text = $"{viewModel.fullPrice}.-";
        }

        private void ToppingSelected(object sender, SelectionChangedEventArgs args)
        {
            var item = args.CurrentSelection[0] as AppItem;
            viewModel.toppingPrice = item.price;
            viewModel.fullPrice = viewModel.CalculateFullPrice();
            FullPrice.Text = $"{viewModel.fullPrice}.-";
        }

        private void SauceSelected(object sender, SelectionChangedEventArgs args)
        {
            var item = args.CurrentSelection[0] as AppItem;
            viewModel.saucePrice = item.price;
            viewModel.fullPrice = viewModel.CalculateFullPrice();
            FullPrice.Text = $"{viewModel.fullPrice}.-";
        }
    }
}