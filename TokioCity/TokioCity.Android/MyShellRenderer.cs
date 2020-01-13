using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TokioCity.Droid;

[assembly: ExportRenderer(typeof(Shell), typeof(MyShellRenderer))]
namespace TokioCity.Droid
{
        class MyShellRenderer : ShellRenderer
        {
            public MyShellRenderer(Context context) : base(context)
            {
            }

            protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
            {
                return new MyShellToolbarAppearanceTracker(this);
            }
        }
    internal class MyShellToolbarAppearanceTracker : IShellToolbarAppearanceTracker
    {
        private MyShellRenderer myShellRenderer;
        public static Android.Widget.Toolbar Mytoolbar;
        public static Android.Support.V7.Widget.Toolbar mytoolbar;
        public MyShellToolbarAppearanceTracker(MyShellRenderer myShellRenderer)
        {
            this.myShellRenderer = myShellRenderer;
            
        }

        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }

        public void ResetAppearance(Android.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker)
        {
            // throw new System.NotImplementedException();
        }

        public void SetAppearance(Android.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
            //throw new System.NotImplementedException();

            Mytoolbar = toolbar;


        }

        public void SetAppearance(Android.Support.V7.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
            
            mytoolbar = toolbar;
            mytoolbar.SetBackgroundColor(Color.FromHex("#181818").ToAndroid());
        }

        public void ResetAppearance(Android.Support.V7.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker)
        {
            throw new NotImplementedException();
        }
    }
}