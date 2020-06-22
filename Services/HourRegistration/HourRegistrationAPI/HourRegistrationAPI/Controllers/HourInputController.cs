using Cassandra;
using HourRegistrationAPI.MessageProducer;
using HourRegistrationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HourRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HourInputController : ControllerBase
    {
        static HourRegistrationProducer _producer = new HourRegistrationProducer();
        static Cluster _cluster = Cluster.Builder()
                .AddContactPoint("cassandra")
                .Build();


        [HttpGet]
        [Route("GetPreviousEntries")]
        public ActionResult<IEnumerable<string>> GetPreviousEntries(string employeeId)
        {
            Guid guid;
            if (!Guid.TryParse(employeeId, out guid))
            {
                return BadRequest("This is not a valid GUID");
            }

            List<HourRegistrationModel> retrievedList = GetListPreviousEntries(employeeId);
            JsonResult result = new JsonResult(retrievedList);
            return result;
        }

        [HttpGet]
        [Route("GetActiveProjects")]
        public ActionResult<IEnumerable<string>> GetActiveProjects(string employeeId)
        {
            Guid guid;
            if (!Guid.TryParse(employeeId, out guid))
            {
                return BadRequest("This is not a valid GUID");
            }

            List<ProjectModel> retrievedList = GetListProjects(employeeId);
            JsonResult result = new JsonResult(retrievedList);
            return result;
        }

        [HttpPost]
        [Route("RegisterHours")]
        public async Task<IActionResult> RegisterHours(HourRegistrationModel input)
        {
            bool result = await Task.Run(() => _producer.ProduceHourRegistrationMessage(input));
            if (!result)
            {
                return BadRequest("Hour registration message could not be sent, is your input data valid?");
            }

            return StatusCode(200);

        }

        List<HourRegistrationModel> GetListPreviousEntries(string employeeId)
        {
            ISession cassandraSession = GetCassandraSession(_cluster);
            var result = cassandraSession.Execute($"SELECT * FROM uurtjefactuurtje.registered_hours_by_employee WHERE employee_id = {employeeId}");
            List<HourRegistrationModel> resultList = new List<HourRegistrationModel>();
            foreach (var r in result)
            {
                resultList.Add(CreateHourRegistrationEntryFromRow(r));
            }

            return resultList;
        }

        List<ProjectModel> GetListProjects(string employeeId)
        {
            ISession cassandraSession = GetCassandraSession(_cluster);
            var result = cassandraSession.Execute($"SELECT * FROM uurtjefactuurtje.projects_by_employee WHERE employee_id = {employeeId}");
            List<ProjectModel> resultList = new List<ProjectModel>();
            foreach (var r in result)
            {
                resultList.Add(CreateProjectEntryFromRow(r));
            }

            return resultList;
        }

        private HourRegistrationModel CreateHourRegistrationEntryFromRow(Row row)
        {
            HourRegistrationModel model = new HourRegistrationModel();

            model.CompanyId = row.GetValue<Guid>("company_id").ToString();
            model.ProjectId = row.GetValue<Guid>("project_id").ToString();
            model.EmployeeId = row.GetValue<Guid>("employee_id").ToString();
            model.Description = row.GetValue<string>("description");

            LocalDate startDate = row.GetValue<LocalDate>("start_date");
            LocalDate endDate = row.GetValue<LocalDate>("end_date");
            LocalTime startTime = row.GetValue<LocalTime>("start_time");
            LocalTime endTime = row.GetValue<LocalTime>("end_time");

            DateTime start = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, startTime.Second);
            DateTime end = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, endTime.Second);

            model.StartTime = start;
            model.EndTime = end;

            return model;
        }

        private ProjectModel CreateProjectEntryFromRow(Row row)
        {
            ProjectModel model = new ProjectModel();

            model.CompanyId = row.GetValue<Guid>("company_id").ToString();
            model.ProjectId = row.GetValue<Guid>("project_id").ToString();
            model.CompanyName = row.GetValue<string>("company_name");
            model.ProjectName = row.GetValue<string>("project_name");

            return model;
        }

        static ISession GetCassandraSession(Cluster cluster)
        {
            bool success = false;
            ISession session = null;

            while (!success)
            {
                try
                {
                    session = cluster.Connect();
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Couldn't connect to the cluster {e.Message}");
                }
            }

            return session;
        }

    }
}