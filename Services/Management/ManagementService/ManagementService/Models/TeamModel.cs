using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagementService.Models
{
    public class TeamModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<PeopleModel> EmployeesInTeam { get; set; } = new List<PeopleModel>();
    }
}
