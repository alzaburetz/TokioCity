using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels;
using TokioCity.Models;

namespace TokioCity.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WokList : ContentView
    {
        public WokListViewModel viewModel;
        public WokList()
        {
            BindingContext = viewModel = new WokListViewModel();
            viewModel.LoadMyWoks.Execute(null);
            InitializeComponent();
        }

        private void IncreaseToppingCount(object sender, EventArgs e)
        {
            var imgbtn = (ImageButton)sender as ImageButton;
            var wok = viewModel.MyWoks.First<MyProduct>(x => x.Id.ToString() == imgbtn.Parent.Parent.Parent.Parent.ClassId);
            var component = wok.Components.First<AppItem>(x => x.uid == imgbtn.ClassId);
            component.Amount++;
            component.price2 = component.price * (int)component.Amount;
            wok.Cost += component.price;
            //viewModel.toppings.First<AppItem>(x => x.uid == imgbtn.ClassId).Amount++;
        }

        private void DecreaseToppingCount(object sender, EventArgs e)
        {
            var imgbtn = (ImageButton)sender as ImageButton;
            var wok = viewModel.MyWoks.First<MyProduct>(x => x.Id.ToString() == imgbtn.Parent.Parent.Parent.Parent.ClassId);
            var component = wok.Components.First<AppItem>(x => x.uid == imgbtn.ClassId);
            if (component.Amount > 1)
            {
                component.Amount--;
                component.price2 = component.price * (int)component.Amount;
                wok.Cost -= component.price;
            }
            else
            {
                wok.Components.Remove(component);
                wok.Cost -= component.price;
            }

        }
    }
}