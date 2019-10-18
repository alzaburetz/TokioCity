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
using CarouselView.FormsPlugin.Abstractions;

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

            LoadCategoriesCommand = new Command(async () =>
            {
                data = await RequestHelper.GetData<List<Category>>(client, "data/app_categs.php?version=");
                CatList.Clear();
                if (data != null)
                    foreach (Category category in data)
                    {
                        category.image = "https://www.tokyo-city.ru" + category.image;
                        if (category.subcategories != null)
                            CatList.Add(category);
                    }
                CurrentCategory = CatList[0].id;

                LoadItemsCommand.Execute(null);
            });

            LoadItemsCommand = new Command(async () =>
            {
                var hash = await RequestHelper.GetData<string>(client, "/data/app_items.php?hash=1&version=");
                if (!Application.Current.Properties.ContainsKey("Hash"))
                {
                    items = await RequestHelper.GetData<List<AppItem>>(client, "/data/app_items.php?version=");
                    if (items != null)
                    {
                        DataBase.WriteAll("Items", items);
                    }
                    Application.Current.Properties.Add("Hash", hash);
                }
                else if (hash != Application.Current.Properties["Hash"].ToString())
                {
                    items = await RequestHelper.GetData<List<AppItem>>(client, "/data/app_items.php?version=");
                    if (items != null)
                    {
                        DataBase.WriteAll("Items", items);
                    }
                    Application.Current.Properties["Hash"] = hash;
                }
                items.Clear();
                LoadCategoryItemsCommand.Execute(null);
            });

            LoadCategoryItemsCommand = new Command(async () =>
            {
                int count = DataBase.GetRecordCount<AppItem>("Items");
                var query = Query.Where("category", category => category.AsArray.Contains(CurrentCategory));
                var itemsFound = DataBase.GetByQueryEnumerable<AppItem>("Items", query);

                Products.Clear();
                List<AppItem> tempList = new List<AppItem>();
                while (itemsFound.MoveNext())
                {

                    Products.Add(itemsFound.Current);
                }
                if (CatList[CategoryIndex].subcategories.Count > 0)
                {
                    SubCats.Clear();
                    foreach (var subcat in CatList[CategoryIndex].subcategories)
                    {
                        SubCats.Add(subcat);
                    }
                }
                itemsFound.Dispose();
            });

            LoadFirstItemCommand = new Command(() =>
            {
                MainProduct = DataBase.GetOneOfCategory<AppItem>("Items", CurrentCategory);
            });
        }
    }
}