using System;
using System.Collections.Generic;

using Xamarin.Forms;

using TokioCity.Views;

namespace TokioCity
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("cart", typeof(Cart));
            Routing.RegisterRoute("categories", typeof(CategoriesTabs));
            Routing.RegisterRoute("categories/product", typeof(Product));
            SetTabBarIsVisible(this, false);
        }
    }
}
