using System;
using System.Collections.ObjectModel;
using System.Text;

namespace TokioCity.ViewModels
{
    public class CreateOrderViewModel: BaseViewModel
    {
        public ObservableCollection<string> tabs { get; set; }
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
        }
    }
}
