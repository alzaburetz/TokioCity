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

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            int index = Children.IndexOf(CurrentPage);
            if (index == 0)
            {
                MessagingCenter.Send<Object>(this, "Favorite");
            }
            else if (index == 1)
            {
                MessagingCenter.Send<Object>(this, "Lunches");

            }
            else if (index == 2)
            {
                MessagingCenter.Send<Object>(this, "Pasta");
            }
            else if (index == 4)
            {
                MessagingCenter.Send<Object>(this, "Wok");
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
            
            Children.Add(new Favorite() { IconImageSource = "favorite" });
            
            Children.Add(new Lunches() { IconImageSource = "lunch" });
            
            Children.Add(new Pasta() { IconImageSource = "pasta" });
            
            Children.Add(new CategoryPage(new int[] { 2227, 2228, 2230, 2231, 2232 }, "Суши") { IconImageSource = "sushi" });
            
            Children.Add(new Woks() { IconImageSource = "wok" });
            
            Children.Add(new Burgers() { IconImageSource = "burger" });
            
            Children.Add(new CategoryPage(new int[] { 48, 36, 61, 62 }, "Роллы") { Title = "Роллы", IconImageSource = "roll" });
            
            Children.Add(new CategoryPage(new int[] { 205 }, "Сеты") { IconImageSource = "set" });
            
            Children.Add(new CategoryPage(new int[] { 211, 213, 2495, 220 }, "Пицца") { IconImageSource = "pizza" });
            
            Children.Add(new CategoryPage(new int[] { 2284 }, "Десерты") { IconImageSource = "dessert.png" });
            
            Children.Add(new CategoryPage(new int[] { 1804 }, "Детское меню") { Title = "Детское меню", IconImageSource = "kids" });
            
            Children.Add(new CategoryPage(new int[] { 1553 }, "Пиццета") { IconImageSource = "pizzetta" });
            
            Children.Add(new CategoryPage(new int[] { 2243 }, "Супы") { IconImageSource = "soup.png" });
            
            Children.Add(new CategoryPage(new int[] { 2259 }, "Горячие блюда") { IconImageSource = "hot.png" });
            
            Children.Add(new CategoryPage(new int[] { 2240 }, "Салаты") { IconImageSource = "salad.png" });
            
            Children.Add(new CategoryPage(new int[] { 233, 289 }, "Напитки") { IconImageSource = "drink.png" });
            
            Children.Add(new CategoryPage(new int[] { 859, 858, 374, 1419 }, "Пироги, торты") { IconImageSource = "pies.png" });
            
            Children.Add(new CategoryPage(new int[] { 52 }, "Горячие закуски") { IconImageSource = "snack.png" });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCategoriesCommand.Execute(null);
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void GoToCart(object sender, EventArgs args)
        {
            await Shell.Current.GoToAsync("cart");
        }
    }
}