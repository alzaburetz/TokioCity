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
        private bool _wokCreated;
        public CollectionView tabs
        {
            get
            {
                return Tabs;
            }
            set
            {
                Tabs = value;
            }
        }
        public bool WokCreated
        {
            get
            {
                return _wokCreated;
            }
            set
            {
                _wokCreated = value;
                if (value == true)
                {
                    MainContent.Children.Clear();
                }
            }
        }
        public Woks()
        {
            BindingContext = viewModel = new WoksViewModel();
            MessagingCenter.Subscribe<Object>(this, "Wok", async (obj) =>
            {
                await Task.Delay(100);
                viewModel.LoadSubcatsCommand.Execute(null);
                tabs.SelectedItem = viewModel.tabs[0];
            });
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        private void ProductSelected(object sender, SelectionChangedEventArgs args)
        {
            try
            {
                var item = args.CurrentSelection[0] as AppItem;
                viewModel.mainPrice = item.price;
                viewModel.fullPrice = viewModel.CalculateFullPrice();
                foreach (var product in viewModel.main)
                {
                    if (product.selected)
                    {
                        product.selected = false;
                    }
                }
                var select = viewModel.main.First<AppItem>(x => x == item);
                select.selected = !select.selected;
            }
            catch { }
            ((CollectionView)sender).SelectedItem = null;
            
        }

        private void MeatSelected(object sender, SelectionChangedEventArgs args)
        {
            try
            {
                var item = args.CurrentSelection[0] as AppItem;
                viewModel.meatPrice = item.price;
                viewModel.fullPrice = viewModel.CalculateFullPrice();
                foreach (var product in viewModel.meat)
                {
                    if (product.selected)
                    {
                        product.selected = false;
                    }
                }
                var select = viewModel.meat.First<AppItem>(x => x == item);
                select.selected = !select.selected;
            }
            catch { }
            ((CollectionView)sender).SelectedItem = null;
            
        }

        private void ToppingSelected(object sender, SelectionChangedEventArgs args)
        {
            try
            {
                var item = args.CurrentSelection[0] as AppItem;
                var select = viewModel.toppings.First<AppItem>(x => x == item);
                if (select.selected)
                {
                    viewModel.toppingPrice -= item.price;
                }
                else
                { 
                    viewModel.toppingPrice += item.price;
                }
                select.selected = !select.selected;
                viewModel.fullPrice = viewModel.CalculateFullPrice();
            }
            catch { }
            ((CollectionView)sender).SelectedItem = null;
            
        }

        private void SauceSelected(object sender, SelectionChangedEventArgs args)
        {
            try
            {
                var item = args.CurrentSelection[0] as AppItem;
                viewModel.saucePrice = item.price;
                viewModel.fullPrice = viewModel.CalculateFullPrice();
                foreach (var product in viewModel.sauce)
                {
                    if (product.selected)
                    {
                        product.selected = false;
                    }
                }
                var select = viewModel.sauce.First<AppItem>(x => x == item);
                select.selected = !select.selected;
            }
            catch { }
            ((CollectionView)sender).SelectedItem = null;
            
        }

        private void SelectWokType(object sender, SelectionChangedEventArgs args)
        {
            var selection = args.CurrentSelection[0];
            if (selection.ToString() == "СОБРАТЬ ВОК")
            {
                MainContent.Children.Clear();
                MainContent.Children.Add(new TokioCity.Views.Components.WokCreate());
            }
            else if (selection.ToString() == "МОИ ВОКИ")
            {
                MainContent.Children.Clear();
                MainContent.Children.Add(new TokioCity.Views.Components.WokList());
            }
        }
    }
}