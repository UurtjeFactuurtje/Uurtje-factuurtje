using App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{

    class ItemService : IDataStore<Item>
    {

        const string Url = "https://172.17.162.241:32776/api/hourinput/";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            //if (string.IsNullOrEmpty(authorizationKey))
            //{
            //    authorizationKey = await client.GetStringAsync(Url + "login");
            //    authorizationKey = JsonConvert.DeserializeObject<string>(authorizationKey);
            //}

            //client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(item),
                Encoding.UTF8, "application/json"));

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

        public Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
