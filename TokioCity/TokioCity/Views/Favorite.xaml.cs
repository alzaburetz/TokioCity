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
    public partial class Favorite : ContentPage
    {
        public FavoriteViewModel favViewModel;
        public Favorite()
        {
            BindingContext = favViewModel = new FavoriteViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            favViewModel.LoadFavorites.Execute(null);
        }

        private async void OpenProduct(object sender, SelectionChangedEventArgs args)
        {
            try
            {
                var item = ((AppItem)args.CurrentSelection[0] as AppItem);
                
            }
            catch { }
            var Collection = (CollectionView)sender;
            Collection.SelectedItem = null;
        }
    }
}