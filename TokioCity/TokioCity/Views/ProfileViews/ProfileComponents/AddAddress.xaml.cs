using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.Models;
using TokioCity.ViewModels.ProfileViewModels;

namespace TokioCity.Views.ProfileViews.ProfileComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAddress : ContentPage
    {
        AddAddressViewModel viewModel { get; set; }
        public AddAddress()
        {
            BindingContext = viewModel = new AddAddressViewModel();
            InitializeComponent();
        }

        private async void GoBack(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void AddAddressClick(object sender, EventArgs args)
        {
            var addr = new Address(
                City.Text,
                Street.Text,
                House.Text,
                int.Parse(Building.Text),
                int.Parse(Flat.Text),
                Comment.Text);
            viewModel.AddAddress.Execute(addr);
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}