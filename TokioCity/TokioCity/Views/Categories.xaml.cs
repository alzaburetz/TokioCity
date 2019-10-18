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

        public async void OpenProducts(object sender, SelectionChangedEventArgs args)
        {
            var item = args.CurrentSelection[0];
            if (item.GetType() == typeof(Category))
            {
                viewModel.CurrentCategory = ((Category)item).id;
                var currentObj = viewModel.CatList.Single<Category>(x => x.id == viewModel.CurrentCategory);
                viewModel.CategoryIndex = viewModel.CatList.IndexOf(currentObj);
            }
            viewModel.LoadCategoryItemsCommand.Execute(null);
        }

        public void CategorySwiped(object sender, SwipedEventArgs e)
        {
            if (e.Direction == SwipeDirection.Left)
            {
                var str = "Left";
                
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCategoriesCommand.Execute(null);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var str = "Tapped";
            var type = sender.GetType();
        }

        private async void CarouselView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            var index = e.CenterItemIndex;
            
            if (currentitem != index)
            {
                var cat = viewModel.CatList[index];
                viewModel.CurrentCategory = cat.id;
                viewModel.CategoryIndex = index;
                currentitem = index;
                viewModel.LoadCategoryItemsCommand.Execute(null);
            }
            
        }

    }
}