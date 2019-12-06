using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.Controls;

namespace TokioCity.Views.CartViews.OrderSteps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Payment : ContentView
    {
        public Payment()
        {
            
            InitializeComponent();
            MainPage = new ContentView
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Children = {
new CustomRadioButton {
Checked = true,
Text = "Daily DotNet Tips!"
}
}
                }
            };
        }
    }
}