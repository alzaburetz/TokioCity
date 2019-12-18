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
using System.Linq;

namespace TokioCity.ViewModels
{
    
    public class BaseCategoryViewModel: BaseViewModel
    {
        public Command LoadProducts { get; set; }
        public Command ClearProducts { get; set; }
        public Command AddFavorite { get; set; }
        public Command AddToCart { get; set; }
        public Command LoadToppings { get; set; }
        private Command ReloadCategories { get; set; }
        public Command LoadProductSubcatd { get; set; }
        public Command ItemTapped { get; set; }
        public ObservableCollection<SubcategorySimplified> subcats { get; set; }
        public CategorySimplified category { get; set; }
        private ObservableCollection<AppItem> _Products;
        public ObservableCollection<AppItem> products { get; set; }
        public ObservableCollection<AppItem> Products
        {
            get
            {
                return _Products;
            }
            set
            {
                _Products = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<AppItem> Toppings { get; set; }
        
        public ToolbarItem cart { get; set; }
        private SubcategorySimplified selectedCategory;
        public SubcategorySimplified SelectedCategory { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public BaseCategoryViewModel(int[] category)
        {
            width = App.screenWidth / 4;
            height = (App.screenHeight / 2);
            products = new ObservableCollection<AppItem>();
            subcats = new ObservableCollection<SubcategorySimplified>();
            Products = new ObservableCollection<AppItem>();
            Toppings = new ObservableCollection<AppItem>();
            selectedCategory = new SubcategorySimplified();
            SelectedCategory = new SubcategorySimplified();
            ClearProducts = new Command(() =>
            {
                    products.Clear();
                    Products.Clear();
            });
            LoadProducts = new Command(async () =>
            {
                products.Clear();
                subcats.Clear();
                try
                {
                    ReloadCategories.Execute(category[0]);
                } catch(IndexOutOfRangeException e) { }
                this.category = DataBase.GetItem<CategorySimplified>("Categories", Query.EQ("cat_id", category[0]));
                    foreach (var cat in category)
                    {
                        var database = await DataBase.GetByQueryEnumerableAsync<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains(cat)));
                        var data = database.GetEnumerator();
                        while(data.MoveNext())
                        {
                            products.Add(data.Current);
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
                try
                {
                    
                    var subcats = await DataBase.GetItemAsync<CategorySimplified>("Categories", LiteDB.Query.EQ("cat_id", (int)categ));
                    foreach (var subcat in subcats.subcats)
                    {
                        this.subcats.Add(subcat);
                    }
                    this.SelectedCategory = this.subcats[0];
                } 
                catch (ArgumentOutOfRangeException e) { }
                

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
                Products.Clear();
                var items = await DataBase.GetByQueryEnumerableAsync<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains((int)categ)));
                var ie = items.GetEnumerator();
                while (ie.MoveNext())
                {
                    Products.Add(ie.Current);
                }
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
