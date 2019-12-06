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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rolls : ContentPage
    {
        public BaseCategoryViewModel viewModel { get; set; }
        public Rolls()
        {
            BindingContext = viewModel = new BaseCategoryViewModel(new int[] { 48, 36, 61, 62 });
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadProducts.Execute(null);
        }

        protected async void OpenProduct(object sender, SelectionChangedEventArgs args)
        {
            try
            {
                var item = ((AppItem)args.CurrentSelection[0] as AppItem);
                await Navigation.PushModalAsync(new Product(item));
            }
            catch { }
            var Collection = (CollectionView)sender;
            Collection.SelectedItem = null;
        }

        protected void LoadSubcat(object sender, SelectionChangedEventArgs args)
        {
            var item = (Subcategory)args.CurrentSelection[0] as Subcategory;
            viewModel.LoadProductSubcatd.Execute(item.id);
        }
    }
}