using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App.Services;
using App.Views;

namespace App
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }

        public App()
        {
            InitializeComponent();
            if (IsUserLoggedIn)
            {
                //MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                DependencyService.Register<ItemService>();
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
