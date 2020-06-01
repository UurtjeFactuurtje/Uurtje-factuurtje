using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementService.Models
{
    public class TeamModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<PeopleModel> EmployeesInTeam { get; set; }
    }
}
