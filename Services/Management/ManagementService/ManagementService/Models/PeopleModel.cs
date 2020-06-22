using System;
using System.ComponentModel.DataAnnotations;

namespace ManagementService.Models
{
    public class PeopleModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
