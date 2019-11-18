using System;
using System.Collections.ObjectModel;
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
    public partial class Sushi : ContentPage
    {
        public ObservableCollection<AppItem> products;
        public BaseCategoryViewModel viewModel;
        public Sushi()
        {
            InitializeComponent();
            products = new ObservableCollection<AppItem>();
            BindingContext = viewModel = new BaseCategoryViewModel(new int[] {47, 74, 92, 102, 204 });
        }

        protected override void OnAppearing()
        {
            viewModel.LoadProducts.Execute(null);
            base.OnAppearing();
            System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(100));
            Subcats.SelectedItem = viewModel.SelectedCategory;

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