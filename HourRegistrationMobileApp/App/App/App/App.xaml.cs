using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App.Services;
using App.Views;

namespace App
{
    public partial class App : Application
    {
        public static string EVENT_LAUNCH_LOGIN_PAGE = "EVENT_LAUNCH_LOGIN_PAGE";
        public static string EVENT_LAUNCH_MAIN_PAGE = "EVENT_LAUNCH_MAIN_PAGE";

        public static bool IsUserLoggedIn { get; set; }

        public App()
        {
            InitializeComponent();
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                DependencyService.Register<ItemService>();
                MainPage = new MainPage();
            }

            MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_LOGIN_PAGE, SetLoginPageAsRootPage);
            MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_MAIN_PAGE, SetMainPageAsRootPage);
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

        private void SetLoginPageAsRootPage(object sender)
        {
            MainPage = new LoginPage();
        }

        private void SetMainPageAsRootPage(object sender)
        {
            MainPage = new MainPage();
        }
    }
}
