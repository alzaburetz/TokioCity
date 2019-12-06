using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using TokioCity.ViewModels.RestrauntViewModels;

namespace TokioCity.Views.Restraunts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Restraunt : ContentPage
    {
        RestrauntViewModel viewModel;
        int id;
        public Restraunt(int id)
        {
            this.id = id;
            BindingContext = viewModel = new RestrauntViewModel(id);
            viewModel.LoadRestraunt.Execute(this.id);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void GoBack(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private void Call(object sender, EventArgs args)
        {
            PhoneDialer.Open(viewModel.Restraunt.phone);
        }
    }
}