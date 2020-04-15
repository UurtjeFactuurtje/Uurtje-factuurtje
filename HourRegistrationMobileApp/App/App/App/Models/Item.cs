using System;

namespace App.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string ProjectId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Day
        {
            get
            {
                return this.Date.DayOfWeek.ToString();
                    }
        }
    }
}