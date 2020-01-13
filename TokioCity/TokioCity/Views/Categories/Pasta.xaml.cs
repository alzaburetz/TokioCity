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
    public partial class Pasta : ContentPage
    {
        public BaseCategoryViewModel viewModel;
        public Pasta()
        {
            BindingContext = viewModel = new BaseCategoryViewModel(new int[] { 224 });
            MessagingCenter.Subscribe<Object>(this, "Pasta", (obj) =>
            {
                viewModel.LoadProducts.Execute(null);
            });
            InitializeComponent();
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