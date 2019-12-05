using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels.ProfileViewModels;


namespace TokioCity.Views.ProfileViews.ProfileComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOrders : ContentView
    {
        OrderListViewModel viewModel;
        public ListOrders()
        {
            BindingContext = viewModel = new OrderListViewModel();
            viewModel.LoadOrders.Execute(null);
            InitializeComponent();
        }
    }
}