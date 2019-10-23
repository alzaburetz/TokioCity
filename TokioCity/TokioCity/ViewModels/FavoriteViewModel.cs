using System;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

using TokioCity.Models;

namespace TokioCity.ViewModels
{
    public class FavoriteViewModel: BaseViewModel
    {
        public ObservableCollection<AppItem> favorites { get; set; }
        public Command LoadFavorites { get; set; }

        public FavoriteViewModel()
        {
            favorites = new ObservableCollection<AppItem>();
            LoadFavorites = new Command(() =>
            {
                var fav = DataBase.GetAllStream<AppItem>("Favorite");
                var enumerator = fav.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    favorites.Add(enumerator.Current);
                }
                fav = null;
            });
        }
    }
}
