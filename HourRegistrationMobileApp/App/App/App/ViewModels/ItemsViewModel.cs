﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using App.Models;
using App.Views;
using Newtonsoft.Json;
using System.Net.Http;

namespace App.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            try
            {
                MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
                {
                    var newItem = item as Item;
                    Debug.WriteLine(JsonConvert.SerializeObject(newItem));
                    Items.Add(newItem);
                    await DataStore.AddItemAsync(newItem);
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (Item item in items)
                {
                    Project project = await ProjectDataStore.GetItemAsync(item.ProjectId.ToString());
                    item.ProjectName = project.Name;
                    Items.Add(item);
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