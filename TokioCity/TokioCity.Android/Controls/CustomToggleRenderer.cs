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
using TokioCity.Droid;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(RadioButton), typeof(RadioButtonRenderer))]
namespace TokioCity.Droid
{
#pragma warning disable CS0618 // 'ViewRenderer<CustomRadioButton, RadioButton>.ViewRenderer()" является устаревшим: 'This constructor is obsolete as of version 2.5. Please use ViewRenderer(Context) instead.'
    public class RadioButtonRenderer : ViewRenderer<CustomRadioButton, RadioButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomRadioButton> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged += ElementOnPropertyChanged;
            }
            if (this.Control == null)
            {
                var radButton = new RadioButton(this.Context);
                this.SetNativeControl(radButton);
            }
            Control.Text = e.NewElement.Text;
            Control.Checked = e.NewElement.Checked;
            Element.PropertyChanged += ElementOnPropertyChanged;
        }
        void ElementOnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Checked":
                    Control.Checked = Element.Checked;
                    break;
                case "Text":
                    Control.Text = Element.Text;
                    break;
            }
        }
    }   
#pragma warning restore CS0618 // 'ViewRenderer<CustomRadioButton, RadioButton>.ViewRenderer()" является устаревшим: 'This constructor is obsolete as of version 2.5. Please use ViewRenderer(Context) instead.'
}