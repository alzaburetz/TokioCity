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
using TokioCity.Views.Categories;


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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadCategoriesCommand.Execute(null);
            await Task.Delay(1000);
            Children.Clear();
            Children.Add(new Favorite() { IconImageSource = "favorite" });
            Children.Add(new Lunches() { IconImageSource = "lunch" });
            Children.Add(new Pasta() { IconImageSource = "pasta" });
            Children.Add(new CategoryPage(new int[] { 2227, 2228, 2230, 2231, 2232 }, "Суши") { IconImageSource = "sushi" });
            Children.Add(new Woks() { IconImageSource = "wok" });
            Children.Add(new CategoryPage(new int[] { 48, 36, 61, 62 }, "Роллы") { Title = "Роллы", IconImageSource = "roll" });
            Children.Add(new CategoryPage(new int[] { 1804 }, "Детское меню") { Title = "Детское меню", IconImageSource = "kids" });
        }

        private async void GoToCart(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("cart");
        }
    }
}