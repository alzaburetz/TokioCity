using System.Collections.Generic;
using System.Collections.ObjectModel;
using TokioCity.Models;
using Xamarin.Forms;
using LiteDB;

namespace TokioCity.ViewModels
{
    public class RecepiesViewModel: BaseViewModel
    {
        public ObservableCollection<AppItem> Items;
        public Command LoadItemsCommand;
        public RecepiesViewModel()
        {
            Items = new ObservableCollection<AppItem>();
            LoadItemsCommand = new Command(() =>
            {
                var query = Query.Where("component", x => x.AsString.Equals("1"));
                var data = DataBase.GetByQueryEnumerable<AppItem>("Items", query);
                while (data.MoveNext())
                {
                    Items.Add(data.Current);
                }

            });
        }
    }
}
