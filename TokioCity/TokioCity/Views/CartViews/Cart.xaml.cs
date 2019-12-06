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
            Code.TextChanged += (sender, args) =>
            {
                ((Entry)sender as Entry).Text = ((Entry)sender as Entry).Text.ToUpper();
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCart.Execute(null);
        }


        private void ConfirmDelete(object sender, EventArgs args)
        {
            var content = ((ImageButton)sender as ImageButton).Parent.Parent.Parent.Parent.Parent as StackLayout;
            //bool animated = await content.Children[0].TranslateTo(-80, 0, 1500);
            viewModel.itemToRemove = ((ImageButton)sender as ImageButton).CommandParameter as CartItem;
            
            content.ForceLayout();
            content.Children[0].Margin = new Thickness(-100, 0, 0, 0);
            var DeleteButton = new ImageButton()
            {
                BackgroundColor = Color.FromHex("#181818"),
                Command = new Command(() =>
                {
                    viewModel.RemoveFromCart.Execute(null);
                }),
                WidthRequest = 100,
                Source = "delete.png"
            };
            content.Children.Add(DeleteButton);
            content.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    content.Children.Remove(DeleteButton);
                    content.Children[0].Margin = new Thickness(0);
                    await content.TranslateTo(0, 0, 1500);
                })
            });
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var curr = Shell.Current;
            string endpoint = $"cart/create?price={viewModel.cartObject.FullCost}";
            await Shell.Current.GoToAsync(endpoint);
        }
    }
}