using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.Services;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CounterTest : ContentPage
    {
        private int counter { get; set; }
        public CounterTest()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, cart, counter.ToString(), Color.Red, Color.White);
            counter++;
        }
    }
}