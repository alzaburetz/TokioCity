using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using LiteDB;

using System.Collections.ObjectModel;

using TokioCity.Models;
using TokioCity.Services;

using FFImageLoading.Forms;

namespace TokioCity.ViewModels
{
    
    public class BaseCategoryViewModel: BaseViewModel
    {
        public Command LoadProducts { get; set; }
        public Command AddFavorite { get; set; }
        public Command AddToCart { get; set; }
        public Command LoadToppings { get; set; }
        private Command ReloadCategories { get; set; }
        public Command LoadProductSubcatd { get; set; }
        public Command ItemTapped { get; set; }
        public ObservableCollection<Subcategory> subcats { get; set; }
        public ObservableCollection<AppItem> products { get; set; }
        public ObservableCollection<AppItem> Products { get; set; }
        public ObservableCollection<AppItem> Toppings { get; set; }
        
        public ToolbarItem cart { get; set; }
        private Subcategory selectedCategory;
        public Subcategory SelectedCategory { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public BaseCategoryViewModel()
        {
            AddFavorite = new Command((item) =>
            {
                DataBase.WriteAll<AppItem>("Favorite", new List<AppItem>() { (AppItem)item as AppItem });
            });
        }
        public BaseCategoryViewModel(int[] category)
        {
            width = App.screenWidth / 4;
            height = (App.screenHeight / 2);
            products = new ObservableCollection<AppItem>();
            subcats = new ObservableCollection<Subcategory>();
            Products = new ObservableCollection<AppItem>();
            Toppings = new ObservableCollection<AppItem>();
            selectedCategory = new Subcategory();
            SelectedCategory = new Subcategory();
            LoadToppings = new Command(async (toppings) =>
            {
                Toppings.Clear();
                List<string> ids = (List<string>)toppings as List<string>;
                foreach (var id in ids)
                {
                    var query = Query.Where("uid", x => x.AsString == id.ToString());
                    var item = DataBase.GetProduct<AppItem>("Items", id.ToString());
                    Toppings.Add(item);
                    await System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(200));
                }
            });
            LoadProducts = new Command(async () =>
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
                        await System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(50));
                    }
                }
            });
            AddFavorite = new Command((item) =>
            {
              
                var check = DataBase.GetProduct<AppItem>("Favorite", (item as AppItem).uid);
                if (check == null)
                {
                    DataBase.WriteItem<AppItem>("Favorite", (AppItem)item as AppItem);
                    (item as AppItem).Favorite = true;
                    DataBase.UpdateItem<AppItem>("Items", null, (item as AppItem));
                }
                else
                {
                    DataBase.RemoveItem<AppItem>("Favorite", Query.Where("uid", x => x.AsString == check.uid));
                    (item as AppItem).Favorite = false;
                    DataBase.UpdateItem<AppItem>("Items", null, (item as AppItem));
                }
                    
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
                    try
                    {
                        this.SelectedCategory = this.subcats[0];
                        LoadProductSubcatd.Execute(SelectedCategory.id);
                    } catch { }
                }
            });

            AddToCart = new Command(async (item) =>
            {
                var Item = (AppItem)item as AppItem;
                await Task.Run(()=>
                {
                    AddToCartMethod(Item);
                });
                
            });



            LoadProductSubcatd = new Command(async (categ) =>
            {
                int count = DataBase.GetRecordCount<AppItem>("Items");
                var query = Query.Where("category", x => x.AsArray.Contains((int)categ));
                var itemsFound = DataBase.GetByQueryEnumerable<AppItem>("Items", query);
                Products.Clear();
                while (itemsFound.MoveNext())
                {
                    //await System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(200));
                    Products.Add(itemsFound.Current);
                }
                itemsFound.Dispose();
            });

            

            ItemTapped = new Command((param) =>
            {
                var item = param.GetType();
            });

        }

        private void AddToCartMethod(AppItem Item)
        {

            var cart = DataBase.GetAllStream<CartItem>("Cart");
            CartItem cartItem = new CartItem(Item, new List<AppItem>(), 1);
            var ie = cart.GetEnumerator();
            while (ie.MoveNext())
            {
                if (ie.Current.Item != null && ie.Current.Item.uid == Item.uid)
                {
                    ie.Current.Count++;
                    DataBase.UpdateItem<CartItem>("Cart", null, ie.Current);
                    return;
                }
            }
            DataBase.WriteItem<CartItem>("Cart", cartItem);
        }
    }
}
