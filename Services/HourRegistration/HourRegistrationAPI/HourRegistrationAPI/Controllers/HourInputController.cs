using HourRegistrationAPI.MessageProducer;
using HourRegistrationAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<IActionResult> RegisterHours(HourRegistrationModel input)
        {
            bool result = await Task.Run(() => producer.ProduceHourRegistrationMessage(input));
            if (!result)
            {
                return BadRequest();
            }

            return StatusCode(200);

        }

    }
}