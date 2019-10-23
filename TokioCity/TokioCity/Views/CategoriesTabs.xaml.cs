using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesTabs : TabbedPage
    {
        
        
        public CategoriesTabs()
        {
            BarBackgroundColor = Color.FromHex("#181818");
            BarTextColor = Color.White;
            InitializeComponent();
        }
    }
}