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
    public partial class Page1 : ContentPage
    {
        public int MyValue { get; set; }
        public Page1()
        {
            MyValue = 1;
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems[0], $"{MyValue}", Color.Red, Color.White);
            MyValue++;
        }
    }
}