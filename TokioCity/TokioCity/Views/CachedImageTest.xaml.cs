using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using FFImageLoading.Forms;

using TokioCity.ViewModels;

namespace TokioCity.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CachedImageTest : ContentPage
    {
        BaseCategoryViewModel viewModel { get; set; }
        public CachedImageTest()
        {
            viewModel = new BaseCategoryViewModel(new int[] { 2227, 2228, 2230, 2231, 2232 });
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            StackLayout stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10
            };
            viewModel.LoadProducts.Execute(null);
            var config = new FFImageLoading.Config.Configuration()
            {
                ExecuteCallbacksOnUIThread = true
            };
            
            foreach (var product in viewModel.products)
            {
                CachedImage img = new CachedImage()
                {
                    Source = string.Format("https://www.tokyo-city.ru/{0}", product.image),
                    CacheType = FFImageLoading.Cache.CacheType.All,
                    FadeAnimationEnabled = false,
                    DownsampleToViewSize = true,
                    DownsampleHeight = 100,
                    LoadingPriority = FFImageLoading.Work.LoadingPriority.Highest,
                    Aspect = Aspect.AspectFit
                };
                stack.Children.Add(img);
            }

            Content = new ScrollView()
            {
                Content = stack
            };
            IsBusy = false;
        }
    }
}