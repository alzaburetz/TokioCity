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

        public CategoryPage(int[] categories, string title = "", bool showSubcats = true) : this()
        {
            BindingContext = viewModel = new BaseCategoryViewModel(categories, showSubcats);
            viewModel.Title = title;
            this.Title = title;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadProducts.Execute(null);
            MainProducts.ItemsSource = viewModel.products;
            Grid.ItemsSource = viewModel.Products;
            await Task.Delay(400);
            Subcats.SelectedItem = viewModel.SelectedCategory;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //viewModel.ClearProducts.Execute(null);
        }

        protected async void OpenProduct(object sender, SelectionChangedEventArgs args)
        {
            //try
            //{
                var item = ((AppItem)args.CurrentSelection[0] as AppItem);
                await Navigation.PushModalAsync(new Product(item));
            //}
            //catch { }
            //var Collection = (CollectionView)sender;
            //Collection.SelectedItem = null;
        }

        protected void LoadSubcat(object sender, SelectionChangedEventArgs args)
        {
            var item = (SubcategorySimplified)args.CurrentSelection[0] as SubcategorySimplified;
            //await Task.Delay(TimeSpan.FromMilliseconds(100));
            viewModel.LoadProductSubcatd.Execute(item.subcat_id);
        }

        private async void CachedImage_Finish(object sender, FFImageLoading.Forms.CachedImageEvents.FinishEventArgs e)
        {
            var a = e.ScheduledWork;
            return;
            await ((FFImageLoading.Forms.CachedImage)sender).ScaleTo(1.5f, 500);
        }
    }
}