using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FFImageLoading.Forms;

using TokioCity.ViewModels;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CachedImageTest : ContentPage
    {
        BaseCategoryViewModel viewModel { get; set; }
        public CachedImageTest()
        {
            BindingContext = viewModel = new BaseCategoryViewModel(new int[] { 2227, 2228, 2230, 2231, 2232 });
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadProducts.Execute(null);
        }
    }
}