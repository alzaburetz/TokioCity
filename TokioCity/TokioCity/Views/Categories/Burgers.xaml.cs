using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels;
using TokioCity.Models;
using TokioCity.Services;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Burgers : ContentPage
    {
        public BurgersViewModel viewModel;
        public int counter { get; set; }
        public Burgers()
        {
            BindingContext = viewModel = new BurgersViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadBurgers.Execute(null);

        }

        private async void SelectBurger(object sender, SelectionChangedEventArgs args)
        {
            try
            {
                var item = args.CurrentSelection[0] as AppItem;
                await Navigation.PushModalAsync(new Product(item));
            }
            catch { }
            var Collection = (CollectionView)sender;
            Collection.SelectedItem = null;
            
        }

        private void AddToFavorite(object sender, EventArgs args)
        {
            viewModel.AddToFavorite.Execute(null);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}