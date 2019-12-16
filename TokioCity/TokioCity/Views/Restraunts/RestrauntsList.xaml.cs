using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

using TokioCity.ViewModels.RestrauntViewModels;
using TokioCity.Views.Restraunts;
using TokioCity.Services;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RestrauntsList : ContentPage
    {
        RestrauntListViewModel viewModel { get; set; }
        Map map { get; set; }
        public RestrauntsList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.tokyo-city.ru");
            BindingContext = viewModel = new RestrauntListViewModel(client);
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            
            base.OnAppearing();
            await Task.Delay(500);
            viewModel.LoadRestraunts.Execute(null);
            if (mapContent.Children.Count == 0)
            {
                map = await MapBuilder.CreateMap(viewModel.Restraunts.ToList(), true);
                mapContent.Children.Add(map);
            }
        }

        private async void OpenRestraunt(object sender, EventArgs e)
        {
            var restrauntId = ((Frame)sender as Frame).ClassId;
            await Shell.Current.Navigation.PushModalAsync(new Restraunt(int.Parse(restrauntId)));
        }
    }
}