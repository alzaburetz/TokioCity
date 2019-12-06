using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels;
using TokioCity.Views.ProfileViews.ProfileComponents;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        ProfileViewModel viewModel { get; set; }
        public Profile()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.tokyo-city.ru");
            BindingContext = viewModel = new ProfileViewModel(client);

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadOffers.Execute(null);
        }

        private void TabsPressed(object sender, SelectionChangedEventArgs args)
        {
            var tab = args.CurrentSelection[0];
            if (tab.ToString() == "БОНУСЫ")
            {
                MainContent.Children.Clear();
                (Bottom.Parent.Parent as StackLayout).IsVisible = false;
            }
            if (tab.ToString() == "ЛИЧНОЕ")
            {
                MainContent.Children.Clear();
                MainContent.Children.Add(new UserData());
                (Bottom.Parent.Parent as StackLayout).IsVisible = true;
                Bottom.Children.Clear();
                Bottom.Children.Add(new Label()
                {
                    TextColor = Color.White,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = "Сохранить данные",
                    FontSize = 14
                }); 
            }
            if (tab.ToString() == "ЗАКАЗЫ")
            {
                MainContent.Children.Clear();
                MainContent.Children.Add(new ListOrders());
                (Bottom.Parent.Parent as StackLayout).IsVisible = false;
            }
            if (tab.ToString() == "АДРЕСА ДОСТАВКИ")
            {
                MainContent.Children.Clear();
                MainContent.Children.Add(new Addresses());
                (Bottom.Parent.Parent as StackLayout).IsVisible = false;
            }

        }
    }
}