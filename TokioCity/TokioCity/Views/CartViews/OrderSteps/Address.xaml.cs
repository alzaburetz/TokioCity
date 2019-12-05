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
            get => Content;
        }
        public Address()
        {
            BindingContext = viewModel = new AddressViewModel();
            InitializeComponent();
        }

        private void OpenMap(object sender, EventArgs args)
        {
            Content.Children.Clear();
            Content.Children.Add(new Takeout());
            ((Label)sender as Label).Opacity = 1;
            Addr.Opacity = 0.5f;
        }

        private void OpenAddr(object sender, EventArgs args)
        {
            Content.Children.Clear();
            Content.Children.Add(new Delivery());
            ((Label)sender as Label).Opacity = 1;
            Self.Opacity = 0.5f;
        }
    }
}