using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using TokioCity.Models;
using TokioCity.Views;
using TokioCity.Services;
using System.Net.Http;

using SQLite;

namespace TokioCity.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        public ObservableCollection<Category> CatList { get; set; }
        public Command LoadCategoriesCommand { get; set; }
        public Command LoadItemsCommand { get; set; }
        public List<Category> data { get; set; }
        public CategoriesViewModel(HttpClient client)
        {
            CatList = new ObservableCollection<Category>();
            LoadCategoriesCommand = new Command(async () =>
            {
                data = await RequestHelper.GetData<List<Category>>(client, "data/app_categs.php?version=1.8");
                CatList.Clear();
                if (data != null)
                foreach(Category category in data)
                {
                    CatList.Add(category);
                }
            });

            
        }

        
    }
}