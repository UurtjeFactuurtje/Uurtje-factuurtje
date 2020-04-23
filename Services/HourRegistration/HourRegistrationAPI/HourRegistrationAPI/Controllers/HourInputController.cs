using HourRegistrationAPI.MessageProducer;
using HourRegistrationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cassandra;
using System;

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
        public ActionResult<IEnumerable<string>> GetPreviousHourEntriesForEmployee(string employeeId)
        {
            List<HourRegistrationModel> retrievedList = getPreviousEntries(employeeId);
            JsonResult result = new JsonResult(retrievedList);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterHours(HourRegistrationModel input)
        {
            bool result = await Task.Run(() => _producer.ProduceHourRegistrationMessage(input));
            if (!result)
            {
                return BadRequest();
            }

            return StatusCode(200);

        }

        List<HourRegistrationModel> getPreviousEntries(string employeeId)
        {
            ISession cassandraSession = GetCassandraSession(_cluster);
            var result = cassandraSession.Execute($"SELECT * FROM uurtjefactuurtje.hours WHERE employee_id = {employeeId} ALLOW FILTERING;");
            List<HourRegistrationModel> resultList = new List<HourRegistrationModel>();
            foreach (var r in result)
            {
                resultList.Add(CreateModelFromRow(r));
            }

            return resultList;
        }

        private HourRegistrationModel CreateModelFromRow(Row row)
        {
            HourRegistrationModel model = new HourRegistrationModel();

            model.Id = row.GetValue<Guid>("id").ToString();
            model.CompanyId= row.GetValue<Guid>("company_id").ToString();
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