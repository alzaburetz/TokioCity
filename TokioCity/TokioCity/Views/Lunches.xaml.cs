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
    public partial class Lunches : ContentPage
    {
        public LunchesViewModel viewModel { get; set; }
        public Lunches()
        {
            BindingContext = viewModel = new LunchesViewModel();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadLunchesCommand.Execute(null);
        }
    }
}