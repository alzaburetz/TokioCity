using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace TokioCity.ViewModels.CartViewModels
{
    public class AddressViewModel: BaseViewModel
    {
        public ObservableCollection<string> tabs { get; set; }
        public string SelectedItem { get; set; }

        public AddressViewModel()
        {
            tabs = new ObservableCollection<string>()
            {
                "По адресу",
                "Самовывоз"
            };
            SelectedItem = tabs[0];
        }
    }
}
