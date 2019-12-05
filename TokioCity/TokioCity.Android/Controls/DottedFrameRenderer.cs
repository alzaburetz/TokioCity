using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Graphics.Drawables;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform;
using Xamarin;

using TokioCity.Controls;
using TokioCity.Droid.Controls;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DottedFrame),typeof(DottedFrameRenderer))]
namespace TokioCity.Droid.Controls
{
    class DottedFrameRenderer: Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public DottedFrameRenderer(Context context):base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            DottedFrame customFrame = Element as DottedFrame;
            float r = customFrame.CornerRadius;
            GradientDrawable shape = new GradientDrawable();
            shape.SetCornerRadii(new float[] { r, r, r, r, r, r, r, r });
            shape.SetColor(Android.Graphics.Color.Transparent);
            shape.SetStroke(2, customFrame.BorderColor.ToAndroid(), 2f, 20f);
            Control.SetBackground(shape);
        }

    }
}