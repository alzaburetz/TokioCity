using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TokioCity.Droid
{
    [Activity(Label = "TokioCity", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            //Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            var widthPixels = Resources.DisplayMetrics.WidthPixels;//getting the width in pixels  
            var scale = Resources.DisplayMetrics.Density;//density i.e., pixels per inch or cms  
            var width = (double)((widthPixels - 0.5f) / scale);//width in units  
            var heightPixels = Resources.DisplayMetrics.HeightPixels;////getting the height in pixels  
            var height = (double)((heightPixels - 0.5f) / scale);//height in units  
            App.screenHeight = (int)height;
            App.screenWidth = (int)width;
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}