using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using TokioCity.Models;
using TokioCity.Views;
using TokioCity.Services;
using System.Net.Http;

using LiteDB;
using System.IO;
using System.Threading.Tasks;

namespace TokioCity.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {

        public ObservableCollection<Category> CatList { get; set; }
        public ObservableCollection<AppItem> Products { get; set; }
        public ObservableCollection<Subcategory> SubCats { get; set; }
        public Command LoadCategoriesCommand { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command LoadCategoryItemsCommand { get; set; }
        public Command LoadFirstItemCommand { get; set; }
        public Command LoadSubcat { get; set; }
        public List<Category> data { get; set; }
        public List<AppItem> items { get; set; }
        public AppItem MainProduct { get; set; }

        public int CurrentCategory { get; set; }
        public int CategoryIndex { get; set; }

        public CategoriesViewModel(HttpClient client)
        {
            CatList = new ObservableCollection<Category>();
            Products = new ObservableCollection<AppItem>();
            SubCats = new ObservableCollection<Subcategory>();
            items = new List<AppItem>();
            data = new List<Category>();
            MainProduct = new AppItem();

            LoadSubcat = new Command(async (subcat) =>
            {
                var items = DataBase.GetByQueryEnumerable<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains((int)subcat)));
                if (Products == null)
                    Products = new ObservableCollection<AppItem>();
                Products.Clear(); 
                if (items != null)
                while (items.MoveNext())
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(200));
                    Products.Add(items.Current);
                }
            });

            LoadCategoriesCommand = new Command(async () =>
            {
                if (DataBase.GetRecordCount<CategorySimplified>("Categories") == 0)
                    data = await RequestHelper.GetData<List<Category>>(client, "data/app_categs_full20.php?version=");
                CatList.Clear();
                var categories = new List<CategorySimplified>();
                if (data.Count > 0)
                {
                    foreach (var category in data)
                    {
                        categories.Add(new CategorySimplified(category));
                    }
                    DataBase.WriteAll<CategorySimplified>("Categories", categories);
                        foreach (Category category in data)
                        {
                            category.image = "https://www.tokyo-city.ru" + category.image;
                            CatList.Add(category);
                        }
                    CurrentCategory = CatList[0].id;

                    LoadItemsCommand.Execute(null);
                }
                else
                {
                    var categs = await DataBase.GetAllStreamAsync<CategorySimplified>("Categories");
                    var ie = categs.GetEnumerator();
                    while(ie.MoveNext())
                    {
                        CatList.Add(new Category(ie.Current));
                    }
                }
               
            });

            LoadItemsCommand = new Command(async () =>
            {
                await Task.Run(() => LoadItemsMethod(client));
            });

            LoadCategoryItemsCommand = new Command(async () =>
            {
                int count = DataBase.GetRecordCount<AppItem>("Items");
                var query = Query.Where("category", category => category.AsArray.Contains(CurrentCategory));
                var itemsFound = DataBase.GetByQueryEnumerable<AppItem>("Items", query);
                SubCats.Clear();
                if (CatList[CategoryIndex].subcategories?.Count > 0)
                {
                    
                    foreach (var subcat in CatList[CategoryIndex].subcategories)
                    {
                        SubCats.Add(subcat);
                    }
                }
                Products.Clear();
                List<AppItem> tempList = new List<AppItem>();
                while (itemsFound.MoveNext())
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(200));
                    Products.Add(itemsFound.Current);
                }
                itemsFound.Dispose();
            });

            LoadFirstItemCommand = new Command(() =>
            {
                MainProduct = DataBase.GetOneOfCategory<AppItem>("Items", CurrentCategory);
            });
        }

        private async void LoadItemsMethod(HttpClient client)
        {
            var hash = await RequestHelper.GetData<string>(client, "/data/app_items.php?hash=1&version=");
            if (!Application.Current.Properties.ContainsKey("Hash"))
            {
                items = await RequestHelper.GetData<List<AppItem>>(client, "/data/app_items_full20.php?version=");
                if (items != null)
                {
                    DataBase.WriteAll("Items", items);
                }
                Application.Current.Properties.Add("Hash", hash);
            }
            else if (hash != Application.Current.Properties["Hash"].ToString())
            {
                items = await RequestHelper.GetData<List<AppItem>>(client, "/data/app_items_full20.php?version=");
                if (items != null)
                {
                    DataBase.WriteAll("Items", items);
                }
                Application.Current.Properties["Hash"] = hash;
            }
            items.Clear();
            LoadCategoryItemsCommand.Execute(null);
        }
    }
}