using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokioCity.ViewModels;
using TokioCity.Models;


namespace TokioCity.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WokCreate : ContentView
    {
        public WoksViewModel viewModel;
        public WokCreate()
        {
            BindingContext = viewModel = new WoksViewModel();
            viewModel.LoadSubcatsCommand.Execute(null);
            InitializeComponent();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
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

        private void CreateWok(object sender, EventArgs args)
        {
            Console.WriteLine(WokName.Text);
            MyProduct wok = new MyProduct();
            wok.Components = new System.Collections.ObjectModel.ObservableCollection<AppItem>();
            wok.Name = WokName.Text;
            wok.Components.Add(viewModel.main.First<AppItem>(x => x.selected == true));
            wok.Components.Add(viewModel.meat.First<AppItem>(x => x.selected == true));
            wok.Components.Add(viewModel.sauce.First<AppItem>(x => x.selected == true));
            foreach (var topping in viewModel.toppings)
            {
                if (topping.selected)
                {
                    wok.Components.Add(topping);
                }
            }
            viewModel.CreateWok.Execute(wok);
            
        }
    }
}