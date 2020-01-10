using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using LiteDB;

using System.Collections.ObjectModel;

using TokioCity.Models;
using TokioCity.Services;
using TokioCity.Extentions;

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
        private ObservableCollectionFast<AppItem> _Products;
        public ObservableCollectionFast<AppItem> products { get; set; }
        public ObservableCollectionFast<AppItem> Products
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
        public int widthGrid { get; set; }
        public int height { get; set; }
        bool subcatsShow { get; set; }
        public BaseCategoryViewModel(int[] category, bool showSubcats = true)
        {
            subcatsShow = showSubcats;
            width = App.screenWidth / 4;
            var info = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo;
            widthGrid = (int)(info.Width / info.Density);
            height = (App.screenHeight / 2);
            products = new ObservableCollectionFast<AppItem>();
            subcats = new ObservableCollection<SubcategorySimplified>();
            Products = new ObservableCollectionFast<AppItem>();
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
                if (subcatsShow)
                {
                    try
                    {
                        ReloadCategories.Execute(category[0]);
                    }
                    catch (IndexOutOfRangeException e) { }
                }
                
                this.category = DataBase.GetItem<CategorySimplified>("Categories", Query.EQ("cat_id", category[0]));
                    foreach (var cat in category)
                    {
                        var database = await DataBase.GetByQueryEnumerableAsync<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains(cat)));
                        var data = database;
                        products.AddRange(database);
                        await Task.Delay(500);
                    }

            });
            AddFavorite = new Command((item) =>
            {
                var inFavorite = (item as AppItem).Favorite;
                (item as AppItem).Favorite = !inFavorite;
                DataBase.UpdateItem<AppItem>("Items", null, (item as AppItem));
                return;
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
                    if (subcats != null)
                    {
                        foreach (var subcat in subcats.subcats)
                        {
                            this.subcats.Add(subcat);
                        }
                        this.SelectedCategory = this.subcats[0];
                    }
                } 
                catch (ArgumentOutOfRangeException e) { }
                

            });

            AddToCart = new Command((item) =>
            {
                var Item = (AppItem)item as AppItem;
                Task.Run(()=>
                {
                    AddToCartMethod(Item);
                });
                
            });

            LoadProductSubcatd = new Command((categ) =>
            {
                Products.Clear();
                var items = DataBase.GetByQuery<AppItem>("Items", Query.Where("category", x => x.AsArray.Contains((int)categ)));
                Products.AddRange(items);
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
