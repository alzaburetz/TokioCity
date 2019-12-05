using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace TokioCity.Controls
{
    public class CustomEntry: Entry
    {
        public CustomEntry(): base()
        {
            this.PlaceholderColor = Color.White;
            this.TextColor = Color.White;
        }
    }
}
