using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            //TODO
        }

		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			var employee = new Employee
			{
				Username = usernameEntry.Text,
				Password = passwordEntry.Text
			};

			var isValid = AreCredentialsCorrect(employee);
			if (isValid)
			{
				App.IsUserLoggedIn = true;
				MessagingCenter.Send<object>(this, App.EVENT_LAUNCH_MAIN_PAGE);

			}
			else
			{
				await DisplayAlert("Alert", "Login failed", "Retry");
				passwordEntry.Text = string.Empty;
			}
		}

		private bool AreCredentialsCorrect(Employee employee)
		{
			return employee.Username == "test" && employee.Password == "test";
		}
	}
}