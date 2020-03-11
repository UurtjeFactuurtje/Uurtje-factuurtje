namespace HourRegistrationAPI.Model
{
    public class HourRegistrationModel
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public int project_id { get; set; }
        public int employee_id { get; set; }
        public int hours { get; set; }
    }
}
