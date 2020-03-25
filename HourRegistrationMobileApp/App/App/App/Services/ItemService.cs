using App.Models;
using Microsoft.DotNet.Cli.Utils.CommandParsing;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{

    class ItemService : IDataStore<Item>
    {

        const string Url = "https://172.17.132.241:32768/api/hourinput/";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient(new HttpClientHandler());
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
