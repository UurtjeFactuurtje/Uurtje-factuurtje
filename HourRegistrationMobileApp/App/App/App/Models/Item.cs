using System;

namespace App.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid EmployeeId { get; set; }
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