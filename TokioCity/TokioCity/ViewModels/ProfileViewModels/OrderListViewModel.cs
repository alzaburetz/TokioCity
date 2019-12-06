using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using LiteDB;

using TokioCity.Models;

namespace TokioCity.ViewModels.ProfileViewModels
{
    public class OrderListViewModel: BaseViewModel
    {
        public ObservableCollection<Cart> Orders { get; set; }
        public Command LoadOrders { get; set; }

        public OrderListViewModel()
        {
            Orders = new ObservableCollection<Cart>();
            LoadOrders = new Command(() =>
            {
                Orders.Clear();
                var orders = DataBase.GetAllStream<Cart>("Orders").GetEnumerator();
                    while (orders.MoveNext())
                    {
                        this.Orders.Add(orders.Current);
                    }
            });
        }
    }
}
