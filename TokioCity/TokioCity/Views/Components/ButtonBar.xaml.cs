using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using TokioCity.Models;

namespace TokioCity.Views.Components
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonBar : ContentView
    {
        public ObservableCollection<CategoryButton> buttons;

        public ButtonBar()
        {
            InitializeComponent();
            List<CategoryButton> btns = new List<CategoryButton>()
            {

            };
        }
    }
}