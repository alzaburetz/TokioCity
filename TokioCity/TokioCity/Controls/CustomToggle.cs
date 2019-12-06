using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace TokioCity.Controls
{
    public class CustomRadioButton : View
    {
        public static readonly BindableProperty CheckedProperty =
 BindableProperty.Create<CustomRadioButton, bool>(
 p => p.Checked, false);
        public static readonly BindableProperty TextProperty =
        BindableProperty.Create<CustomRadioButton, string>(
        p => p.Text, string.Empty);
        public bool Checked
        {
            get
            {
                return (bool)GetValue(CheckedProperty);
            }
            set
            {
                this.SetValue(CheckedProperty, value);
                //if (Command != null) { Command.Execute(this.CommandParameter); }
            }
        }
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                this.SetValue(TextProperty, value);
            }
        }
    }
}
