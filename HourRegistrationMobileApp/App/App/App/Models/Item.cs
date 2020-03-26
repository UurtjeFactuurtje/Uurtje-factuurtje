using System;

namespace App.Models
{
    public class Item
    {
        public String Id { get; set; }
        public int CompanyId { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}