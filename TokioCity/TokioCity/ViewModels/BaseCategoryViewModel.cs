using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using LiteDB;

using System.Collections.ObjectModel;

using TokioCity.Models;
using TokioCity.Services;

namespace TokioCity.ViewModels
{
    
    public class BaseCategoryViewModel: BaseViewModel
    {
        public Command LoadProducts { get; set; }
        public Command AddFavorite { get; set; }
        private Command ReloadCategories { get; set; }
        public Command LoadProductSubcatd { get; set; }
        public ObservableCollection<Subcategory> subcats { get; set; }
        public ObservableCollection<AppItem> products { get; set; }
        public ObservableCollection<AppItem> Products { get; set; }

        public BaseCategoryViewModel()
        {
            AddFavorite = new Command((item) =>
            {
                DataBase.WriteAll<AppItem>("Favorite", new List<AppItem>() { (AppItem)item as AppItem });
            });
        }
        public BaseCategoryViewModel(int[] category)
        {
            products = new ObservableCollection<AppItem>();
            subcats = new ObservableCollection<Subcategory>();
            Products = new ObservableCollection<AppItem>();
            LoadProducts = new Command(() =>
            {
                products.Clear();
                subcats.Clear();
                ReloadCategories.Execute(category[0]);
                foreach(var cat in category)
                {
                    var data = DataBase.GetByQueryEnumerable<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains(cat)));
                    while (data.MoveNext())
                    {
                        products.Add(data.Current);
                    }
                }
            });
            AddFavorite = new Command((item) =>
            {
                DataBase.WriteAll<AppItem>("Favorite", new List<AppItem>() { (AppItem)item as AppItem });
            });

            ReloadCategories = new Command(async (categ) =>
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.tokyo-city.ru");
                    var req = await RequestHelper.GetData<List<Category>>(client, "/data/app_categs.php?version=");
                    foreach (Category cat in req)
                    {
                        if (cat.subcategories != null && cat.id == (int)categ)
                        {
                            foreach (Subcategory sub in cat.subcategories)
                            {
                                this.subcats.Add(sub);
                            }
                            break;
                        }
                    }
                }
            });

            LoadProductSubcatd = new Command(async (categ) =>
            {
                int count = DataBase.GetRecordCount<AppItem>("Items");
                var query = Query.Where("category", x => x.AsArray.Contains((int)categ));
                var itemsFound = DataBase.GetByQueryEnumerable<AppItem>("Items", query);
                Products.Clear();
                while (itemsFound.MoveNext())
                {
                    await System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(200));
                    Products.Add(itemsFound.Current);
                }
                itemsFound.Dispose();
            });

        }
    }
}
