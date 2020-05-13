using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
