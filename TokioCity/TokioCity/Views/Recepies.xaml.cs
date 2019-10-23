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
    public partial class Recepies : ContentPage
    {
        private RecepiesViewModel viewModel;
        public Recepies()
        {
            InitializeComponent();
            BindingContext = viewModel = new RecepiesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}