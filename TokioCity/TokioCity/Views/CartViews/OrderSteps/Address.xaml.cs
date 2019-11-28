﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TokioCity.ViewModels.CartViewModels;

namespace TokioCity.Views.CartViews.OrderSteps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Address : ContentView
    {
        AddressViewModel viewModel;
        public Address()
        {
            BindingContext = viewModel = new AddressViewModel();
            InitializeComponent();
        }
    }
}