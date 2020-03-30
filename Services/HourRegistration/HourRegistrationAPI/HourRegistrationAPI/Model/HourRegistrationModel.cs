using System.ComponentModel.DataAnnotations;

namespace HourRegistrationAPI.Model
{
    public class HourRegistrationModel
    {
        public int id { get; set; }
        [Required]
        public int? company_id { get; set; }
        [Required]
        public int? project_id { get; set; }
        [Required]
        public int? employee_id { get; set; }
        [Required]
        public int? hours { get; set; }
    }
}
