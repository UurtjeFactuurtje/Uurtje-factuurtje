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
        const string Url = "http://192.168.2.23:80/api/ProjectModels";

        public Task<Project> AddItemAsync(Project item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(String id)
        {
            throw new NotImplementedException();
        }

        async public Task<Project> GetItemAsync(String id)
        {
            try
            {
                var response = await App.Client.GetAsync(Url+"/"+id);
                Debug.WriteLine(Url + "/" + id);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(response);
                }

                Debug.WriteLine(await response.Content.ReadAsStringAsync());

                return JsonConvert.DeserializeObject<Project>(
                    await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }

        async public Task<IEnumerable<Project>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var response = await App.Client.GetAsync(Url);
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
