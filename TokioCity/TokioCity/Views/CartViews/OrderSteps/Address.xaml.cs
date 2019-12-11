using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels.CartViewModels;

namespace TokioCity.Views.CartViews.OrderSteps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Address : ContentView
    {
        AddressViewModel viewModel;
        public StackLayout ContentPublic
        {
            get => MainContent;
        }
        public Address()
        {
            BindingContext = viewModel = new AddressViewModel();
            InitializeComponent();
        }

        private void OpenMap(object sender, EventArgs args)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new Takeout());
            ((Label)sender as Label).Opacity = 1;
            Addr.Opacity = 0.5f;
        }

        private void OpenAddr(object sender, EventArgs args)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new Delivery());
            ((Label)sender as Label).Opacity = 1;
            Self.Opacity = 0.5f;
        }
    }
}