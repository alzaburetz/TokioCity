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
    public partial class Cart : ContentPage
    {
        public CartViewModel viewModel { get; set; }
        public Cart()
        {
            BindingContext = viewModel = new CartViewModel();
            try
            {
                var state = Shell.Current.CurrentState;
                var a = state;
            } catch { }
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCart.Execute(null);
        }
    }
}