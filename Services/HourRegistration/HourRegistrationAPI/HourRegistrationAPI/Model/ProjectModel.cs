using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
