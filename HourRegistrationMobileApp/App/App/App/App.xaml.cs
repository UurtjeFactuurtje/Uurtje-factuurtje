using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App.Services;
using App.Views;
using Xamarin.Auth;
using System.Net.Http;
using System.Linq;
using Xamarin.Auth.Presenters;
using System.Diagnostics;

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
            MainPage = new MainPage();

            DependencyService.Register<ItemService>();
            DependencyService.Register<ProjectService>();

            //MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_LOGIN_PAGE, SetLoginPageAsRootPage);
            //MessagingCenter.Subscribe<object>(this, EVENT_LAUNCH_MAIN_PAGE, SetMainPageAsRootPage);
        }
        public static Account AuthAccount { get; set; }
        public static HttpClient Client = new HttpClient(new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                //bypass
                return true;
            }
        }, false);
        protected override void OnStart()
        {
            var oAuth = new OAuth2AuthenticatorEx("xamarin-client", "offline_access hourregistrationapi",
                new Uri("http://192.168.2.23:32772/connect/authorize"), new Uri("http://192.168.2.23:32772/grants"))
            {
                AccessTokenUrl = new Uri("http://192.168.2.23:32772/connect/token"),
                ShouldEncounterOnPageLoading = false
            };
            var account = AccountStore.Create().FindAccountsForService("AuthServer");
            if (account != null && account.Any())
            {
                AuthAccount = account.First();
                Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthAccount.Properties["access_token"]}");
            }
            else
            {
                try
                {
                    var presenter = new OAuthLoginPresenter();
                    presenter.Completed += Presenter_Completed;
                    presenter.Login(oAuth);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        private void Presenter_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {

                AuthAccount = e.Account;
                Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthAccount.Properties["access_token"]}");
                //    await AccountStore.Create().SaveAsync(e.Account, "AuthServer");
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE);
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
