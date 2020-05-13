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

    class ProjectService : IDataStore<Project>
    {
        const string Url = "https://192.168.2.20:32770/api/ProjectModels/";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient(new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    //bypass
                    return true;
                }
            }, false);
            //if (string.IsNullOrEmpty(authorizationKey))
            //{
            //    authorizationKey = await client.GetStringAsync(Url + "login");
            //    authorizationKey = JsonConvert.DeserializeObject<string>(authorizationKey);
            //}

            //client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public Task<Project> AddItemAsync(Project item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        async public Task<IEnumerable<Project>> GetItemsAsync(bool forceRefresh = false)
        {
            HttpClient client = GetClient();
            try
            {
                var response = await client.GetAsync(Url);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(response);
                }

                Debug.WriteLine(await response.Content.ReadAsStringAsync());

                return JsonConvert.DeserializeObject<List<Project>>(
                    await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        public Task<bool> UpdateItemAsync(Project item)
        {
            throw new NotImplementedException();
        }
    }
}
