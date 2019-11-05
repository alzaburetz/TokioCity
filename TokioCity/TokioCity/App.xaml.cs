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
        public Command AddToFavorite { get; set; }
        public Command RemoveFromFavorite { get; set; }
        public static int screenHeight, screenWidth;
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<DataBaseService>();
            MainPage = new AppShell();
            AddToFavorite = new Command((item) =>
            {
                Favorite.AddToFavorite(null, null, (AppItem)item);
            });
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
