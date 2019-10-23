using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels;

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
    }
}