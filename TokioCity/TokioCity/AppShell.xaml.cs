using System;
using System.Collections.Generic;

using Xamarin.Forms;

using TokioCity.Views;

namespace TokioCity
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("cart", typeof(Cart));
            Routing.RegisterRoute("cart/create", typeof(CreateOrder));
            Routing.RegisterRoute("categories", typeof(CategoriesTabs));
            Routing.RegisterRoute("categories/product", typeof(Product));

            SetTabBarIsVisible(this, false);
        }
    }
}
