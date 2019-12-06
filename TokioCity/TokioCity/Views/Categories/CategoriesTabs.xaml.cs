using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels;
using System.Net.Http;

using AndroidSpec = Xamarin.Forms.PlatformConfiguration.AndroidSpecific;


namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesTabs : TabbedPage
    {
        
        public CategoriesViewModel viewModel { get; set; }
        public static int ProductCounter { get; set; }
        public ToolbarItem Cart
        {
            get
            {
                return MenuItem1;
            }
            
        }
        public CategoriesTabs()
        {
            AndroidSpec.TabbedPage.SetOffscreenPageLimit(this, 10);
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.tokyo-city.ru");
            BindingContext = viewModel = new CategoriesViewModel(client);
            BarBackgroundColor = Color.FromHex("#181818");
            BarTextColor = Color.White;
            
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadCategoriesCommand.Execute(null);
        }

        private async void GoToCart(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("cart");
        }
    }
}