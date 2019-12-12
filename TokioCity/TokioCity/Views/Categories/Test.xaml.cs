using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokioCity.ViewModels;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage
    {
        BaseCategoryViewModel viewModel { get; set; }
        public Test()
        {
            BindingContext = viewModel = new BaseCategoryViewModel(new int[] { 211, 213, 2495, 220, 214 });
            viewModel.Title = "Test";
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadProducts.Execute(null);
        }
        private void OpenProduct(object sender, EventArgs args)
        {

        }
    }
}