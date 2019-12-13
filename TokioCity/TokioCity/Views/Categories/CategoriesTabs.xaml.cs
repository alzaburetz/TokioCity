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
            await Task.Delay(50);
            Children.Add(new Favorite() { IconImageSource = "favorite" });
            await Task.Delay(50);
            Children.Add(new Lunches() { IconImageSource = "lunch" });
            await Task.Delay(50);
            Children.Add(new Pasta() { IconImageSource = "pasta" });
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 2227, 2228, 2230, 2231, 2232 }, "Суши") { IconImageSource = "sushi" });
            await Task.Delay(50);
            Children.Add(new Woks() { IconImageSource = "wok" });
            await Task.Delay(50);
            Children.Add(new Burgers() { IconImageSource = "burger" });
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 48, 36, 61, 62 }, "Роллы") { Title = "Роллы", IconImageSource = "roll" });
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 205 }, "Сеты") { IconImageSource = "set" });
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 211, 213, 2495, 220, 214 }, "Пицца") { IconImageSource = "pizza" });
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 2284 }, "Десерты") { IconImageSource = "dessert.png" });
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 1804 }, "Детское меню") { Title = "Детское меню", IconImageSource = "kids" });
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 1553 }, "Пиццета") { IconImageSource = "pizzetta" });
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 2243}, "Супы") { IconImageSource = "soup.png"});
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 2259 }, "Горячия блюда") { IconImageSource = "hot.png" });
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 2240 }, "Салаты") { IconImageSource = "salad.png"});
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 233, 289 }, "Напитки") { IconImageSource = "drink.png" });
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 859, 858, 374, 1419 }, "Пироги, торты") { IconImageSource = "pies.png" });
            await Task.Delay(50);
            Children.Add(new CategoryPage(new int[] { 52 }, "Горячие закуски") { IconImageSource = "snack.png" });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Children.Clear();
        }

        private async void GoToCart(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("cart");
        }
    }
}