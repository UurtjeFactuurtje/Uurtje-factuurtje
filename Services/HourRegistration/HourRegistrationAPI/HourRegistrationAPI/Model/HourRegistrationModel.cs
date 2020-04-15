using System;
using System.ComponentModel.DataAnnotations;

namespace HourRegistrationAPI.Model
{
    [Serializable]
    public class HourRegistrationModel
    {
        public string Id { get; set; }

        [Required]
        public string CompanyId { get; set; }

        [Required]
        public string ProjectId { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public string Description { get; set; }
    }
}
