using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokioCity.Models;
using TokioCity.ViewModels;

namespace TokioCity.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Product : ContentPage
    {
        public ProductViewModel viewModel { get; set; }
        public string i;

        public Product(AppItem item)
        {
            BindingContext = viewModel = new ProductViewModel();
            viewModel.product = item;
            InitializeComponent();
        }

        public Product()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadProductCommand.Execute(viewModel.product.uid);
            bool hasToppings = viewModel.product.toppings.Count > 1;
            Toppings.IsVisible = hasToppings;
        }

        public async void GoBack(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private void Add(object sender, EventArgs args)
        {
            viewModel.amount++;
            Amount.Text = viewModel.amount.ToString();
        }

        private void Reduce(object sender, EventArgs args)
        {
            if (viewModel.amount > 0)
            viewModel.amount--;
            Amount.Text = viewModel.amount.ToString();

        }

        public void OpenCalories(object sender, EventArgs args)
        {
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    await CaloriesCard.TranslateTo(0, 500, 3000);
            //});
            CaloriesCard.IsVisible = true;
            
        }

        private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            await CaloriesCard.TranslateTo(0, 220, 3000);
            CaloriesCard.IsVisible = false;
        }

        private void SelectToppings(object sender, SelectionChangedEventArgs args)
        {
            try
            {
                var item = args.CurrentSelection[0];
                (item as AppItem).selected = !(item as AppItem).selected;
                if ((item as AppItem).selected)
                    viewModel.AddToppingCommand.Execute(item);
                else
                    viewModel.RemoveToppingCommand.Execute(item);
            }
            catch { }
            var Collection = (CollectionView)sender;
            Collection.SelectedItem = null;
        }

        private void IncreaseToppingCount(object sender, EventArgs e)
        {
            var imgbtn = (ImageButton)sender as ImageButton;
            viewModel.selectedToppings.First<AppItem>(x => x.uid == imgbtn.ClassId).Amount++;
            //viewModel.toppings.First<AppItem>(x => x.uid == imgbtn.ClassId).Amount++;
        }

        private void DecreaseToppingCount(object sender, EventArgs e)
        {
            var imgbtn = (ImageButton)sender as ImageButton;
            var amnt = viewModel.selectedToppings.First<AppItem>(x => x.uid == imgbtn.ClassId).Amount;
            if (amnt > 1)
            {
                viewModel.selectedToppings.First<AppItem>(x => x.uid == imgbtn.ClassId).Amount--;
                //viewModel.toppings.First<AppItem>(x => x.uid == imgbtn.ClassId).Amount--;
            }
            else
            {
                var itemToremove = viewModel.selectedToppings.First<AppItem>(x => x.uid == imgbtn.ClassId);
                viewModel.selectedToppings.Remove(itemToremove);
                viewModel.toppings.First<AppItem>(x => x.uid == imgbtn.ClassId).selected = false;
            }
                
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            CaloriesCard.IsVisible = false;
        }

        private async void OpenCart(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            Shell.Current.CurrentItem = new Cart();
        }

        private async void AddToCart(object sender, EventArgs args)
        {
            await Shell.Current.DisplayAlert("Успешно!", "Товар успешно добавлен в корзину", "ОК");
            viewModel.AddToCart.Execute(null);
        }
    }
}