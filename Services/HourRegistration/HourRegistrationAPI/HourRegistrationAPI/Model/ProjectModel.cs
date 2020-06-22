using System;
using System.ComponentModel.DataAnnotations;

namespace HourRegistrationAPI.Model
{
    [Serializable]
    public class ProjectModel
    {
        [Required]
        public string CompanyId { get; set; }

        [Required]
        public string ProjectId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string ProjectName { get; set; }

    }
}
