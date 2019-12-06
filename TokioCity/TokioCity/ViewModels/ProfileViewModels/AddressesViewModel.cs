using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

using TokioCity.Models;
using LiteDB;

namespace TokioCity.ViewModels.ProfileViewModels
{
    public class AddressesViewModel: BaseViewModel
    {
        public ObservableCollection<Address> Addresses { get; set; }
        public ObservableCollection<string> FormattedAdresses { get; set; }
        public Command RemoveAddress { get; set; }
        public Command LoadAddresses { get; set; }

        public AddressesViewModel()
        {
            Addresses = new ObservableCollection<Address>();
            FormattedAdresses = new ObservableCollection<string>();
            LoadAddresses = new Command(() =>
            {
                Addresses.Clear();
                FormattedAdresses.Clear();
                var addresses = DataBase.GetAllStream<Address>("Addresses");
                var ie = addresses.GetEnumerator();
                while (ie.MoveNext())
                {
                    Addresses.Add(ie.Current);
                    FormattedAdresses.Add(ie.Current.FormatAddress());
                }
            });
            RemoveAddress = new Command((addr) =>
            {
                FormattedAdresses.Remove(addr.ToString());
                var addrObj = Addresses.First<Address>(x => x.FormatAddress() == addr.ToString());
                DataBase.RemoveItem<Address>("Addresses", Query.Where("_id", x => x.AsInt32 == addrObj.Id));
            });
        }
    }
}
