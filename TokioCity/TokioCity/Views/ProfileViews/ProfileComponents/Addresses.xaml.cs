using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels.ProfileViewModels;


namespace TokioCity.Views.ProfileViews.ProfileComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Addresses : ContentView
    {
        AddressesViewModel viewModel { get; set; }
        public Addresses()
        {
            BindingContext = viewModel = new AddressesViewModel();
            viewModel.LoadAddresses.Execute(null);
            InitializeComponent();
        }

        private async void AddAddress(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PushModalAsync(new AddAddress());
        }
    }
}