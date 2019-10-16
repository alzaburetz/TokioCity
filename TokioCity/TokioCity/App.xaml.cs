using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokioCity.Services;
using TokioCity.Models;
using System.Collections.Generic;

namespace TokioCity
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
