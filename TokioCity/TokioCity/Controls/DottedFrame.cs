using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace TokioCity.Controls
{
    public class DottedFrame: Frame
    {
        public DottedFrame(): base()
        {
            this.CornerRadius = 15.0f;
            this.BorderColor = Color.White;
            this.BackgroundColor = Color.Transparent;
        }

    }
}
