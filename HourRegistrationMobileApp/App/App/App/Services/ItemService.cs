using App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Services
{

    class ItemService : IDataStore<Item>
    {
        const string Url = "http://192.168.2.20:80/api/hourinput/";

        public async Task<Item> AddItemAsync(Item item)
        {
            var response = await App.Client.PostAsync(Url + "RegisterHours",
                new StringContent(JsonConvert.SerializeObject(item),
                Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine(response);
            }

            return JsonConvert.DeserializeObject<Item>(
                await response.Content.ReadAsStringAsync());
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            var param1 = "?EmployeeId=" + HttpUtility.UrlEncode("b6ef7666-716d-46a6-98c6-9c73c43be6ab");
            Debug.WriteLine(Url + "GetPreviousEntries" + param1);
            var response = await App.Client.GetAsync(Url + "GetPreviousEntries" + param1);

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine(response);
            }

            Debug.WriteLine(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<Item>>(
                await response.Content.ReadAsStringAsync());
        }

        public Task<bool> UpdateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
