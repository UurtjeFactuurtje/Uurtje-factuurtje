using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App.Models;
using App.Services;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace App.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        public ObservableCollection<Project> Projects { get; set; }
        public IDataStore<Project> ProjectDataStore => DependencyService.Get<IDataStore<Project>>();
        public Project SelectedProject { get; set; }


        public NewItemPage()
        {
            InitializeComponent();
            Projects = new ObservableCollection<Project>();
            ExecuteLoadItemsCommand();

            Item = new Item
            {
                CompanyId = Guid.NewGuid(),
                EmployeeId = Guid.Parse("b6ef7666-716d-46a6-98c6-9c73c43be6ab"),
                Description = "This is an item description.",
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            
            Item.StartTime = DatePickerView.Date + PickerStartTime.Time;
            Item.EndTime = DatePickerView.Date + PickerEndTime.Time;
            Item.ProjectId = SelectedProject.Id;
            Item.ProjectName = SelectedProject.Name;
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Projects.Clear();
                var items = await ProjectDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Projects.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}