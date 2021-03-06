﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagementService.Models
{
    public class ProjectModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<TeamModel> TeamsOnProject { get; set; } = new List<TeamModel>();
    }
}
