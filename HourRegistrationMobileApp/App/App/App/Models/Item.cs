using System;

namespace App.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Day
        {
            get
            {
                return this.StartTime.DayOfWeek.ToString();
                    }
        }
    }
}