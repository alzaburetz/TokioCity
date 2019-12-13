using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels;
using TokioCity.Models;

namespace TokioCity.Views.Categories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        BaseCategoryViewModel viewModel { get; set; }
        public CategoryPage()
        {
            InitializeComponent();
        }

        public CategoryPage(int[] categories, string title) : this()
        {
            BindingContext = viewModel = new BaseCategoryViewModel(categories);
            viewModel.Title = title;
            this.Title = title;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(500);
            viewModel.LoadProducts.Execute(null);
            //await Task.Delay(1000);
            //IconImageSource = new FFImageLoading.Forms.DataUrlImageSource($"https://www.tokyo-city.ru{viewModel.category.image}");
            
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