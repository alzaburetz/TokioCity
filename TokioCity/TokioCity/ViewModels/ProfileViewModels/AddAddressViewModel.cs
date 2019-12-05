using System;
using System.Collections.Generic;
using System.Text;

using TokioCity.Models;
using Xamarin.Forms;


namespace TokioCity.ViewModels.ProfileViewModels
{
    public class AddAddressViewModel: BaseViewModel
    {
        public Command AddAddress { get; set; }

        public AddAddressViewModel()
        {
            AddAddress = new Command((address) =>
            {
                DataBase.WriteItem<Address>("Addresses", address as Address);
            });
        }
    }
}
