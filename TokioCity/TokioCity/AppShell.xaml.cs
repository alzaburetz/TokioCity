using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TokioCity
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            SetTabBarIsVisible(this, false);
        }
    }
}
