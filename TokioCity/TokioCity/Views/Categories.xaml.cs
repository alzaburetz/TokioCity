using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using TokioCity.Models;

using TokioCity.Services;
using System.Net.Http;
using System.Collections.ObjectModel;
using TokioCity.ViewModels;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Categories : ContentPage
    {
        private HttpClient client;
        private int currentitem;
        CategoriesViewModel viewModel;
        public Categories()
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("https://www.tokyo-city.ru");
            BindingContext = viewModel = new CategoriesViewModel(client);
            currentitem = -1;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCategoriesCommand.Execute(null);
        }

        private async void CarouselView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            var index = e.CenterItemIndex;
            if (e.HorizontalOffset == 0)
            {
                var test = "Test";
            }
            if (currentitem != index)
            {
                var cat = viewModel.CatList[index];
                viewModel.CurrentCategory = cat.id;
                viewModel.CategoryIndex = index;
                currentitem = index;
                viewModel.LoadCategoryItemsCommand.Execute(null);
            }
            
        }

        private void SubcatLoad(object sender, SelectionChangedEventArgs args)
        {
            try 
            { 
                var index = (args.CurrentSelection[0] as Subcategory).id;
                viewModel.LoadSubcat.Execute(index);
                
            }
            catch (ArgumentOutOfRangeException e) { }
            var Collection = (CollectionView)sender;
            Collection.SelectedItem = null;
        }


        private async void OpenProduct(object sender, SelectionChangedEventArgs items)
        {
            try
            {
                var item = ((AppItem)items.CurrentSelection[0] as AppItem);
                await Navigation.PushModalAsync(new Product(item));
            }
            catch { }
            var Collection = (CollectionView)sender;
            Collection.SelectedItem = null;
        }
    }
}