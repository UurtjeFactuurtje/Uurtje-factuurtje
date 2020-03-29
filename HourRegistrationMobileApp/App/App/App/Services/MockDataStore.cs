using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using App.Models;

namespace App.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(),CompanyId = 1, ProjectId=1,EmployeeId = 1, Date = DateTime.Today, StartTime = new TimeSpan(8,30,00), EndTime = new TimeSpan(12,45,00), Text = "Project 1", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(),CompanyId = 1, ProjectId=1,EmployeeId = 1, Date = DateTime.Today, StartTime = new TimeSpan(13,30,00), EndTime = new TimeSpan(14,45,00), Text = "Project 1", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(),CompanyId = 1, ProjectId=2,EmployeeId = 1, Date = DateTime.Today, StartTime = new TimeSpan(14,45,00), EndTime = new TimeSpan(17,00,00), Text = "Project 1", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(),CompanyId = 1, ProjectId=2,EmployeeId = 1, Date = DateTime.Today.AddDays(-1), StartTime = new TimeSpan(8,30,00), EndTime = new TimeSpan(16,45,00), Text = "Project 1", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(),CompanyId = 1, ProjectId=3,EmployeeId = 1, Date = DateTime.Today.AddDays(1), StartTime = new TimeSpan(8,30,00), EndTime = new TimeSpan(12,45,00), Text = "Project 1", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(),CompanyId = 1, ProjectId=3,EmployeeId = 1, Date = DateTime.Today.AddDays(1), StartTime = new TimeSpan(12,45,00), EndTime = new TimeSpan(17,45,00), Text = "Project 1", Description="This is an item description." },
            };
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(item);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}