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
using TokioCity.Controls;
using TokioCity.Droid.Controls;
using Xamarin.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace TokioCity.Droid.Controls
{
    public class CustomEntryRenderer: EntryRenderer
    {
        public CustomEntryRenderer(Context context): base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var entry = Element as CustomEntry;
            Control.SetHintTextColor(Android.Graphics.Color.FloralWhite);
            Control.SetTextColor(Android.Graphics.Color.White);
        }
    }
}