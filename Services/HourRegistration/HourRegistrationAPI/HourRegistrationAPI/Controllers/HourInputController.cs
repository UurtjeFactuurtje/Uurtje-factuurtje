using HourRegistrationAPI.MessageProducer;
using HourRegistrationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HourRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HourInputController : ControllerBase
    {
        HourRegistrationProducer producer = new HourRegistrationProducer();

        [HttpGet]
        public ActionResult<IEnumerable<string>> PostHours()
        {
            return new string[] { "It works" };
        }

        //[HttpPost]
        //public async Task<ActionResult<HourRegistrationModel>> RegisterHours(HourRegistrationModel input)
        //{
        //    producer.ProduceHourRegistrationMessage(input);

        //    return CreatedAtAction(nameof(input), new { id = input.id }, input);
        //}

        [HttpPost]
        public void RegisterHours(HourRegistrationModel input)
        {
            producer.ProduceHourRegistrationMessage(input);
        }
    }
}