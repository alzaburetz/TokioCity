using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels;
using TokioCity.Views.CartViews.OrderSteps;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("cartPrice", "price")]
    public partial class CreateOrder : ContentPage
    {
        CreateOrderViewModel viewModel { get; set; }
        private float _price;
        public string cartPrice
        {
            set
            {
                viewModel.Price = float.Parse(Uri.UnescapeDataString(value));
            }
        }
        public CreateOrder()
        {
            BindingContext = viewModel = new CreateOrderViewModel(_price);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MainContent.Children.Add(new UserData());
            PriceLabel.Text = $"{viewModel.Price} .-";
        }

        private void SelectTab(object sender, SelectedItemChangedEventArgs args)
        {
           
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selection = e.CurrentSelection[0];
            if (((string)selection as string).ToLower() == "доставка")
            {
                MainContent.Children.Clear();
                MainContent.Children.Add(new Address());
            } else if (((string)selection as string).ToLower() == "личное")
            {
                MainContent.Children.Clear();
                MainContent.Children.Add(new UserData());
            }
        }

        private void Continue(object sender, EventArgs args)
        {
            var selection = viewModel.tabs.IndexOf(viewModel.selectedTab)+1;
            if (selection < 2)
            Tabs.SelectedItem = viewModel.tabs[selection];
            if (selection == 1)
            {
                MainContent.Children.Clear();
                MainContent.Children.Add(new Address());
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (viewModel.tabs.IndexOf(viewModel.selectedTab) == 0)
                return base.OnBackButtonPressed();
            else
                return SwitchSelectedItem();
        }

        private bool SwitchSelectedItem()
        {
            Tabs.SelectedItem = viewModel.tabs[viewModel.tabs.IndexOf(viewModel.selectedTab) - 1];
            return true;
        }
    }
}