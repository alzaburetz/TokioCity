using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using TokioCity.Models;
using TokioCity.ViewModels;

namespace TokioCity.Services
{
    public class Favorite: BaseCategoryViewModel
    {
        public static Favorite Instance;
        public Favorite()
        {
            Instance = this;
        }
        public static void AddToFavorite(object sender, EventArgs args, AppItem item)
        {
            var btn = (ImageButton)sender as ImageButton;
            //btn.Source = "heartfilled.png";
            btn.Clicked += (obj, arg) => RemoveFromFavorite(sender, args, item);
            btn.Clicked -= (obj, arg) => AddToFavorite(sender, args,item );
            //DB Stuff
            Instance.AddFavorite.Execute(item);
        }

        public static void RemoveFromFavorite(object sender, EventArgs args, AppItem item)
        {
            var btn = (ImageButton)sender as ImageButton;
            //btn.Source = "heart.png";
            btn.Clicked -= (obj, arg) => RemoveFromFavorite(sender, args,item );
            btn.Clicked += (obj, arg) => AddToFavorite(sender, args,item);
        }
    }
}
