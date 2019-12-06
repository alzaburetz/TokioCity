using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using TokioCity.Models;

namespace TokioCity.ViewModels
{
    public class CreateOrderViewModel: BaseViewModel
    {
        public ObservableCollection<string> tabs { get; set; }
        public Command CreateOrder { get; set; }
        public string selectedTab { get; set; }
        public float Price { get; set; }
        public CreateOrderViewModel(float price)
        {
            Price = price;
            tabs = new ObservableCollection<string>()
            {
                "ЛИЧНОЕ",
                "ДОСТАВКА",
                "ОПЛАТА"
            };
            selectedTab = tabs[0];

            CreateOrder = new Command(async () =>
            {
                await Task.Run(() =>
                {
                    var cart = DataBase.GetAllStream<CartItem>("Cart");
                    var items = new List<CartItem>();
                    var ie = cart.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        items.Add(ie.Current);
                    }
                    Cart order = new Cart(items, 1);
                    order.CalculateFullPrice();

                    DataBase.WriteItem<Cart>("Orders", order);
                    DataBase.RemoveAll<CartItem>("Cart");
                });
                
            });
        }
    }
}
