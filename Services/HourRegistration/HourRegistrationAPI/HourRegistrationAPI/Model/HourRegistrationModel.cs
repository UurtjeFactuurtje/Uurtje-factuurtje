using System;
using System.ComponentModel.DataAnnotations;

namespace HourRegistrationAPI.Model
{
    [Serializable]
    public class HourRegistrationModel
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }

        public string Description { get; set; }
    }
}
