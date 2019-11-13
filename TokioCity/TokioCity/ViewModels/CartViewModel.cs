using System;
using System.Collections.Generic;
using System.Text;

using Xamarin;
using Xamarin.Forms;

using TokioCity.Models;
using System.Collections.ObjectModel;

namespace TokioCity.ViewModels
{
    public class CartViewModel: BaseViewModel
    {
        public ObservableCollection<CartItem> cart { get; set; }

        public Command LoadCart { get; set; }

        public CartViewModel()
        {
            cart = new ObservableCollection<CartItem>();

            LoadCart = new Command(() =>
            {
                cart.Clear();
                var data = DataBase.GetAllStream<CartItem>("Cart");
                var ie = data.GetEnumerator();
                while (ie.MoveNext())
                {
                    cart.Add(ie.Current);
                }
            });
        }
    }
}
